using System;
using System.Collections.Generic;

namespace Application.Users.Entities
{
    public class Group
    {

        public int Id { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public Group()
        {
        }
    }
}
