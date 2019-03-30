using System;
namespace Application.Users.Views.Request
{
    public class CreateUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordVerify { get; set; }
    }
}
