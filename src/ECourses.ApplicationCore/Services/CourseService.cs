using ECourses.ApplicationCore.Common.Constants;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Helpers;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseConverter _courseConverter;
        private readonly ICourseValidator _courseValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly IEntityValidator<Category> _categoryEntityValidator;

        public CourseService(ICourseRepository courseRepository, 
            ICourseConverter courseConverter, 
            ICourseValidator courseValidator, 
            IEntityValidator<Course> courseEntityValidator, 
            IEntityValidator<Author> authorEntityValidator, 
            IEntityValidator<Category> categoryEntityValidator)
        {
            _courseRepository = courseRepository;
            _courseConverter = courseConverter;
            _courseValidator = courseValidator;
            _courseEntityValidator = courseEntityValidator;
            _authorEntityValidator = authorEntityValidator;
            _categoryEntityValidator = categoryEntityValidator;
        }

        public async Task Create(CreateCourseViewModel model)
        {
            _courseValidator.ValidateCreateCourseViewModel(model);
            await _courseEntityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == model.Title.ToLower());

            await _categoryEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.CategoryId);
            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == model.AuthorId);

            var course = _courseConverter.ConvertToCourse(model);

            await _courseRepository.Create(course);
        }

        public async Task Delete(Guid id)
        {
            var course = await _courseRepository.GetById(id);

            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            await _courseRepository.Delete(id);
        }

        public async Task<CourseViewModel> GetCourseById(Guid id)
        {
            var course = await _courseRepository.GetById(id);

            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            return _courseConverter.ConvertToViewModel(course!);
        }

        public async Task<PagedListModel<CourseViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, CourseFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var courses = await _courseRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<CourseViewModel>
            {
                Count = courses.Count,
                Items = courses.Items.Select(c => _courseConverter.ConvertToViewModel(c))
            };
        }

        public async Task Update(UpdateCourseViewModel model)
        {
            _courseValidator.ValidateUpdateCourseViewModel(model);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.Id);
            await _courseEntityValidator.ValidateIfEntityExistsByCondition(c => c.Title == model.Title);

            if (model.CategoryId != Guid.Empty)
            {
                await _categoryEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.CategoryId);
            }

            if (model.AuthorId != Guid.Empty)
            {
                await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == model.AuthorId);
            }

            var course = await _courseRepository.GetById(model.Id);

            course!.Title = model.Title != "" ? model.Title : course.Title;
            course.Description = model.Description != "" ? model.Description : course.Description;
            course.PublishedAt = model.PublishedAt ?? course.PublishedAt;
            course.Price = model.Price ?? course.Price;
            course.CategoryId = model.CategoryId != Guid.Empty ? model.CategoryId : course.CategoryId;
            course.AuthorId = model.AuthorId != Guid.Empty ? model.AuthorId : course.AuthorId;

            await _courseRepository.Update(course);
        }

        private FilterOptions<Course>? MapFilterOptions(CourseFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Course>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(c => c.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.Description is not null)
            {
                predicate = predicate.And(c => c.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            if (filter.CategoryId != Guid.Empty)
            {
                predicate = predicate.And(c => c.CategoryId == filter.CategoryId);
            }

            if (filter.AuthorId != Guid.Empty)
            {
                predicate = predicate.And(c => c.AuthorId == filter.AuthorId);
            }

            if (filter.PublishedAt is not null)
            {
                predicate = predicate.And(c => c.PublishedAt == filter.PublishedAt);
            }

            if (filter.PriceMin is not null)
            {
                predicate = predicate.And(c => c.Price >= filter.PriceMin);
            }

            if (filter.PriceMax is not null)
            {
                predicate = predicate.And(c => c.Price <= filter.PriceMax);
            }

            return new FilterOptions<Course>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Course>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                CourseOrderQueryConstants.TitleAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Title
                },
                CourseOrderQueryConstants.TitleDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Title
                },

                CourseOrderQueryConstants.PriceAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Price
                },
                CourseOrderQueryConstants.PriceDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Price
                },

                CourseOrderQueryConstants.PublishedAtAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.PublishedAt
                },
                CourseOrderQueryConstants.PublishedAtDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.PublishedAt
                },
                _ => null
            };
        }
    }
}
