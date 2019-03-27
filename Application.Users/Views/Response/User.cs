using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Membership> Memberships { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public User(Entities.User user)
        {
            Id = user.Id;
            if (user.Status != null)
            {
                Status = new Status(user.Status);
            }
            Email = user.Email;
            if (user.Role != null)
            {
                Role = new Role(user.Role);
            }
            if (user.Language != null)
            {
                Language = new Language(user.Language);
            }
            Avatar = user.Avatar;
            FirstName = user.FirstName;
            LastName = user.LastName;
            if (user.Memberships?.Count > 0)
            {
                Memberships = user.Memberships.Select(mem => new Membership(mem));
            }
            CreatedAt = user.CreatedAt;
            LastUpdated = user.LastUpdated;
        }
    }
}
