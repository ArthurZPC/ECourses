using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Helpers;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;
using Microsoft.AspNetCore.Http;

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

        public Video ConvertToVideo(CreateVideoViewModel model)
        {
            return new Video
            {
                Title = model.Title,
                Url = FileNameGenerator.Generate(model.Video),
                CourseId = model.CourseId
            };
        }

        public Video ConvertToVideo(UpdateVideoViewModel model)
        {
            return new Video
            {
                Id = model.Id,
                Title = model.Title,
                Url = FileNameGenerator.Generate(model.Video),
                CourseId = model.CourseId
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
