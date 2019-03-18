using System;
namespace Application.Groups.Entities
{
    public class Comment
    {

        public int Id { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }

        public Comment()
        {
        }
    }
}
