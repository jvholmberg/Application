using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Posts.Views.Response
{
    public class Post
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public Category Category { get; set; }

        public Type Type { get; set; }

        public Status Status { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public Post(Entities.Post post)
        {
            Id = post.Id;
            GroupId = GroupId;
            UserId = post.UserId;
            if (post.Category != null)
            {
                Category = new Category(post.Category);
            }
            if (post.Type != null)
            {
                Type = new Type(post.Type);
            }
            if (post.Status != null)
            {
                Status = new Status(post.Status);
            }
            Title = post.Title;
            Text = post.Text;
            if (post.Comments != null)
            {
                Comments = post.Comments.Select(com => new Comment(com));
            }
            CreatedAt = post.CreatedAt;
            LastUpdated = post.LastUpdated;
        }
    }
}
