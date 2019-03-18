using System;

namespace Application.Users.Entities
{
    public class User
    {

        public int Id { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public User()
        {
        }
    }
}
