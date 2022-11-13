using ECourses.ApplicationCore.Features.Commands.Categories;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ICategoryConverter
    {
        Category ConvertToCategory(CategoryViewModel model);
        Category ConvertToCategory(CreateCategoryCommand command);
        Category ConvertToCategory(UpdateCategoryCommand command);
        CategoryViewModel ConvertToViewModel(Category model);
    }
}
