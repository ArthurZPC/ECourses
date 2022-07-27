using ECourses.ApplicationCore.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
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

        public Category ConvertToCategory(CreateCategoryViewModel model)
        {
            return new Category
            {
                Title = model.Title,
            };
        }

        public Category ConvertToCategory(UpdateCategoryViewModel model)
        {
            return new Category
            {
                Id = model.Id,
                Title = model.Title
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
