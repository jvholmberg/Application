using System;
namespace Application.Users.Views.Response
{
    public class Auth
    {
        public string AccessToken { get; set; }

        public long AccessTokenLifetime { get; set; }

        public DateTime AccessTokenExpiry { get; set; }

        public string RefreshToken { get; set; }

        public Auth(string accessToken, Entities.User user)
        {
            AccessToken = accessToken;
            AccessTokenLifetime = user.AccessTokenLifetime;
            AccessTokenExpiry = user.AccessTokenExpiry;
            RefreshToken = user.RefreshToken;
        }
    }
}
