using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IRatingValidator
    {
        void ValidateCreateRatingViewModel(CreateRatingViewModel model);
        void ValidateUpdateRatingViewModel(UpdateRatingViewModel model);
    }
}
