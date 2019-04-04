using System;
namespace Application.Users.Views.Response
{
    public class Auth
    {

        public string AccessToken { get; set; }

        public long AccessTokenLifetime { get; set; }

        public DateTime AccessTokenExpiry { get; set; }

        public string RefreshToken { get; set; }

        public long RefreshTokenLifetime { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }

        public Auth(string accessToken, long accessTokenLifetime, DateTime accessTokenExpiry, Entities.User user)
        {
            AccessToken = accessToken;
            AccessTokenLifetime = accessTokenLifetime;
            AccessTokenExpiry = accessTokenExpiry;
            RefreshToken = user.RefreshToken;
            RefreshTokenLifetime = user.RefreshTokenLifetime;
            RefreshTokenExpiry = user.RefreshTokenExpiry;
        }
    }
}
