using System;
namespace Application.Users.Entities
{
    public class Status
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Status()
        {
        }
    }
    public enum StatusName {
        Inactive,
        Pending,
        Active
    }
}
