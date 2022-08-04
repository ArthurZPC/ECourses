using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IVideoConverter
    {
        Video ConvertToVideo(VideoViewModel model);
        Video ConvertToVideo(CreateVideoViewModel model);
        Video ConvertToVideo(UpdateVideoViewModel model);
        VideoViewModel ConvertToViewModel(Video model);
    }
}
