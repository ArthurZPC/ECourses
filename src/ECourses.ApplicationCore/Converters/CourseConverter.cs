using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Courses;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class CourseConverter : ICourseConverter
    {
        public Course ConvertToCourse(CourseViewModel model)
        {
            return new Course
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublishedAt = model.PublishedAt,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                Price = model.Price
            };
        }

        public Course ConvertToCourse(CreateCourseCommand command)
        {
            return new Course
            {
                Title = command.Title,
                Description = command.Description,
                PublishedAt = command.PublishedAt,
                AuthorId = command.AuthorId,
                CategoryId = command.CategoryId,
                Price = command.Price
            };
        }

        public Course ConvertToCourse(UpdateCourseCommand command)
        {
            return new Course
            {
                Id = command.Id,
                Title = command.Title,
                Description = command.Description,
                PublishedAt = command.PublishedAt,
                AuthorId = command.AuthorId,
                CategoryId = command.CategoryId,
                Price = command.Price
            };
        }

        public CourseViewModel ConvertToViewModel(Course model)
        {
            return new CourseViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublishedAt = model.PublishedAt,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                Price = model.Price
            };
        }
    }
}
