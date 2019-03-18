using System;
namespace Application.Authentication.Entities
{
    public class User
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }

        public int RefreshTokenLifetime { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }

        public User()
        {
        }
    }
}
