using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Posts.Entities
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public enum TypeName
    {
        Promotion,
        Article,
        Thread,
    }
}
