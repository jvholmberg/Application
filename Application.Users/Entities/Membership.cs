using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Entities
{
    public class Membership
    {
        [Key]
        public int Id { get; set; }

        public Status Status { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
