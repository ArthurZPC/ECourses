using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IAuthorValidator
    {
        void ValidateCreateAuthorViewModel(CreateAuthorViewModel model);
        void ValidateUpdateAuthorViewModel(UpdateAuthorViewModel model);
    }
}
