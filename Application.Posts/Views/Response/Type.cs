using System;
namespace Application.Posts.Views.Response
{
    public class Type
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Type(Entities.Type type)
        {
            Id = type.Id;
            Name = type.Name;
        }
    }
}
