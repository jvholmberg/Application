using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Users.Views.Response
{
    public class Group
    {

        public int Id { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public IEnumerable<Membership> Memberships { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public Group(Entities.Group group)
        {
            Id = group.Id;
            if (group.Status != null)
            {
                Status = new Status(group.Status);
            }
            Name = group.Name;
            if (group.Memberships?.Count > 0)
            {
                Memberships = group.Memberships.Select(mem => new Membership(mem));
            }
            CreatedAt = group.CreatedAt;
            LastUpdated = group.LastUpdated;
        }
    }
}
