using System;
namespace Application.Users.Views.Response
{
    public class Auth
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int RefreshTokenLifetime { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }

        public Auth(string accessToken, Entities.User user)
        {
            AccessToken = accessToken;
            RefreshToken = user.RefreshToken;
            RefreshTokenLifetime = user.RefreshTokenLifetime;
            RefreshTokenExpiry = user.RefreshTokenExpiry;
        }
    }
}
