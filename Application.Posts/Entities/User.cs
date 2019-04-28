using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
