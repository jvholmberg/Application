using System;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Core.Util
{

    public interface IJwt
    {
        int? GetUserId(string authorizationHeader);
        string GetUserEmail(string authorizationHeader);
        string GetUserRole(string authorizationHeader);
        string getAccessToken(string authorizationHeader);
    }

    public class Jwt : IJwt
    {

        private readonly JwtSecurityTokenHandler _TokenHandler;

        public Jwt()
        {
            _TokenHandler = new JwtSecurityTokenHandler();
        }

        public int? GetUserId(string authorizationHeader)
        {
            var payload = getPayload(authorizationHeader);
            if (payload.TryGetValue("nameid", out object nameIdObj))
            {
                var nameId = nameIdObj as string;
                if (int.TryParse(nameId, out int userId))
                {
                    return userId;
                }
            }
            return null;
        }

        public string GetUserEmail(string authorizationHeader)
        {
            var payload = getPayload(authorizationHeader);
            if (payload.TryGetValue("unique_name", out object uniqueNameObj))
            {
                var userEmail = uniqueNameObj as string;
                return userEmail;
            }
            return null;

        }

        public string GetUserRole(string authorizationHeader)
        {
            var payload = getPayload(authorizationHeader);
            if (payload.TryGetValue("role", out object roleObj))
            {
                var userRole = roleObj as string;
                return userRole;
            }
            return null;
        }

        public string getAccessToken(string authorizationHeader)
        {
            var bare = authorizationHeader.Replace("Bearer ", "");
            return bare;
        }

        private JwtPayload getPayload(string autorizationHeader)
        {
            var encoded = autorizationHeader.Replace("Bearer ", "");
            var decoded = _TokenHandler.ReadJwtToken(encoded);
            var payload = decoded.Payload;
            return payload;
        }
    }
}
