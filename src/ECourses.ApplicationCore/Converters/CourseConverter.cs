using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
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

        public Course ConvertToCourse(CreateCourseViewModel model)
        {
            return new Course
            {
                Title = model.Title,
                Description = model.Description,
                PublishedAt = model.PublishedAt,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                Price = model.Price
            };
        }

        public Course ConvertToCourse(UpdateCourseViewModel model)
        {
            return new Course
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PublishedAt = model.PublishedAt ?? DateTime.Now,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                Price = model.Price ?? 0
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
