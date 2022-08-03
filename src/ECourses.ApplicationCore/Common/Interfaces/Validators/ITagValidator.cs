using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface ITagValidator
    {
        void ValidateCreateTagViewModel(CreateTagViewModel model);
        void ValidateUpdateTagViewModel(UpdateTagViewModel model);
    }
}
