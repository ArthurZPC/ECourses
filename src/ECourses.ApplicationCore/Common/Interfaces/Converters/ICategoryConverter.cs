using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ICategoryConverter
    {
        Category ConvertToCategory(CategoryViewModel model);
        Category ConvertToCategory(CreateCategoryViewModel model);
        Category ConvertToCategory(UpdateCategoryViewModel model);
        CategoryViewModel ConvertToViewModel(Category model);
    }
}
