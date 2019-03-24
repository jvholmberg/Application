using System;
namespace Application.Users.Views
{
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordVerify { get; set; }
    }
}
