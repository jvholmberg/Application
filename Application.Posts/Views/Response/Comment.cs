using System;
namespace Application.Posts.Views.Response
{
    public class Comment
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        public Comment(Entities.Comment comment)
        {
            Id = comment.Id;
            if (comment.Status != null)
            {
                Status = new Status(comment.Status);
            }
            UserId = comment.UserId;
            Text = comment.Text;
            CreatedAt = comment.CreatedAt;
            LastUpdated = comment.LastUpdated;
        }
    }
}
