using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Entities
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

    }
}
