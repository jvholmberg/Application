using System;
namespace Application.Users.Views.Response
{
    public class Auth
    {

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expiry { get; set; }

        public Auth(string accessToken, DateTime expiry, Entities.User user)
        {
            AccessToken = accessToken;
            RefreshToken = user.RefreshToken;
            Expiry = expiry;
        }
    }
}
