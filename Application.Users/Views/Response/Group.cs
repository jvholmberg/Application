using System;
using System.Collections.Generic;

namespace Application.Users.Views.Response
{
    public class Group
    {

        public int Id { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public Group()
        {
        }
    }
}
