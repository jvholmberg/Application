using System;
namespace Application.Posts.Views.Response
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category SubCategory { get; set; }

        public Category(Entities.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            if (category.SubCategory != null)
            {
                SubCategory = new Category(category.SubCategory);
            }
        }
    }
}
