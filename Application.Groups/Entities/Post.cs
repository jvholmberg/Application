using System;
using System.Collections.Generic;

namespace Application.Groups.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public Post()
        {
        }
    }
}
