using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;
using System.Linq.Expressions;

namespace ECourses.ApplicationCore.Interfaces.Validators
{
    public interface ICategoryValidator
    {
        void ValidateCreateCategoryViewModel(CreateCategoryViewModel model);
        void ValidateUpdateCategoryViewModel(UpdateCategoryViewModel model);
    }
}
