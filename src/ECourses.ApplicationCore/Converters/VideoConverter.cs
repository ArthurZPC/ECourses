using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Videos;
using ECourses.ApplicationCore.Helpers;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class VideoConverter : IVideoConverter
    {
        public Video ConvertToVideo(VideoViewModel model)
        {
            return new Video
            {
                Id = model.Id,
                Title = model.Title,
                Url = model.Url,
                CourseId = model.CourseId
            };
        }

        public Video ConvertToVideo(CreateVideoCommand command)
        {
            return new Video
            {
                Title = command.Title,
                Url = FileNameGenerator.Generate(command.Video),
                CourseId = command.CourseId
            };
        }

        public Video ConvertToVideo(UpdateVideoCommand command)
        {
            return new Video
            {
                Id = command.Id,
                Title = command.Title,
                Url = command.NewUrl,
                CourseId = command.CourseId
            };
        }

        public VideoViewModel ConvertToViewModel(Video model)
        {
            return new VideoViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Url = model.Url,
                CourseId = model.CourseId
            };
        }
    }
}
