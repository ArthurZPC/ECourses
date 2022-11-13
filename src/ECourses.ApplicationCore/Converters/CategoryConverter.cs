using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Categories;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class CategoryConverter : ICategoryConverter
    {
        public Category ConvertToCategory(CategoryViewModel model)
        {
            return new Category
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public Category ConvertToCategory(CreateCategoryCommand command)
        {
            return new Category
            {
                Title = command.Title,
            };
        }

        public Category ConvertToCategory(UpdateCategoryCommand command)
        {
            return new Category
            {
                Id = command.Id,
                Title = command.Title
            };
        }

        public CategoryViewModel ConvertToViewModel(Category model)
        {
            return new CategoryViewModel
            {
                Id = model.Id,
                Title = model.Title,
            };
        }
    }
}
