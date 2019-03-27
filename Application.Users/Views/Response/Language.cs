using System;
namespace Application.Users.Views.Response
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Language(Entities.Language language)
        {
            Id = language.Id;
            Name = language.Name;
            Code = language.Code;
        }
    }
}
