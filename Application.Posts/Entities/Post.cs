using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Entities
{
    public class Post
    {

        [Key]
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public Status Status { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
