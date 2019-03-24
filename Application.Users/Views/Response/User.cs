using System;
using System.Collections.Generic;

namespace Application.Users.Views.Response
{
    public class User
    {

        public int Id { get; set; }

        public Status Status { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public Language Language { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
