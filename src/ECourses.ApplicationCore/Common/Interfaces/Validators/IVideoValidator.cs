using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IVideoValidator
    {
        void ValidateCreateVideoViewModel(CreateVideoViewModel model);
        void ValidateUpdateVideoViewModel(UpdateVideoViewModel model);
    }
}
