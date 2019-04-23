using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public Post Post { get; set; }

        public Status Status { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
