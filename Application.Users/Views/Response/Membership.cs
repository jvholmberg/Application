using System;
namespace Application.Users.Views.Response
{
    public class Membership
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public Membership()
        {
        }
    }
}
