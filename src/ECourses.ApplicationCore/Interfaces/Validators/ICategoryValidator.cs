using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Interfaces.Validators
{
    public interface ICategoryValidator
    {
        Task ValidateCreateCategoryViewModel(CreateCategoryViewModel model);
        Task ValidateUpdateCategoryViewModel(UpdateCategoryViewModel model);
        Task ValidateIfCategoryFound(Guid id);
    }
}
