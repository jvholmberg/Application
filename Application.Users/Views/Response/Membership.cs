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

        public Membership(Entities.Membership membership)
        {
            Id = membership.Id;
            if (membership.Status != null)
            {
                Status = new Status(membership.Status);
            }
            if (membership.Role != null)
            {
                Role = new Role(membership.Role);
            }
            if (membership.User != null)
            {
                User = new User(membership.User);
            }
            if (membership.Group != null)
            {
                Group = new Group(membership.Group);
            }
            CreatedAt = membership.CreatedAt;
            LastUpdated = membership.LastUpdated;
        }
    }
}
