using ECourses.ApplicationCore.Features.Commands.Videos;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IVideoConverter
    {
        Video ConvertToVideo(VideoViewModel model);
        Video ConvertToVideo(CreateVideoCommand command);
        Video ConvertToVideo(UpdateVideoCommand command);
        VideoViewModel ConvertToViewModel(Video model);
    }
}
