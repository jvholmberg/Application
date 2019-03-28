using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Entities
{
    public class Group
    {

        [Key]
        public int Id { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
