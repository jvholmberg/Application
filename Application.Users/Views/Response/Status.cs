using System;
namespace Application.Users.Views.Response
{
    public class Status
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Status(Entities.Status status)
        {
            Id = status.Id;
            Name = status.Name;
        }
    }
}
