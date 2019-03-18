using System;
using System.Collections.Generic;

namespace Application.Groups.Entities
{
    public class Group
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public User Owner { get; set; }

        public IEnumerable<User> Members { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public Group()
        {
        }
    }
}
