using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Entities
{
    public class Status
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public enum StatusName
    {
        Inactive,
        Pending,
        Active
    }
}
