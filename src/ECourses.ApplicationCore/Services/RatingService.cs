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
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingConverter _ratingConverter;
        private readonly IRatingValidator _ratingValidator;
        private readonly IEntityValidator<Rating> _ratingEntityValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IEntityValidator<User> _userEntityValidator;

        public RatingService(IRatingRepository ratingRepository, 
            IRatingConverter ratingConverter,
            IRatingValidator ratingValidator,
            IEntityValidator<Rating> ratingEntityValidator,
            IEntityValidator<Course> courseEntityValidator, 
            IEntityValidator<User> userEntityValidator)
        {
            _ratingRepository = ratingRepository;
            _ratingConverter = ratingConverter;
            _ratingValidator = ratingValidator;
            _ratingEntityValidator = ratingEntityValidator;
            _courseEntityValidator = courseEntityValidator;
            _userEntityValidator = userEntityValidator;
        }

        public async Task Create(CreateRatingViewModel model)
        {
            _ratingValidator.ValidateCreateRatingViewModel(model);
            await _ratingEntityValidator.ValidateIfEntityExistsByCondition(r => r.UserId == model.UserId && r.CourseId == model.CourseId);

            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == model.UserId);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.CourseId);

            var rating = _ratingConverter.ConvertToRating(model);

            await _ratingRepository.Create(rating);
        }

        public async Task Delete(Guid id)
        {
            var rating = await _ratingRepository.GetById(id);

            await _ratingEntityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == id);

            await _ratingRepository.Delete(id);
        }

        public async Task<RatingViewModel> GetRatingById(Guid id)
        {
            var rating = await _ratingRepository.GetById(id);

            await _ratingEntityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == id);

            return _ratingConverter.ConvertToViewModel(rating!);
        }

        public async Task<PagedListModel<RatingViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, RatingFilterQuery? filter)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            var filterOptions = MapFilterOptions(filter);
            var orderOptions = MapOrderOptions(orderField);

            var ratings = await _ratingRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<RatingViewModel>
            {
                Count = ratings.Count,
                Items = ratings.Items.Select(r => _ratingConverter.ConvertToViewModel(r))
            };
        }

        public async Task Update(UpdateRatingViewModel model)
        {
            _ratingValidator.ValidateUpdateRatingViewModel(model);
            await _ratingEntityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == model.Id);
            await _ratingEntityValidator.ValidateIfEntityExistsByCondition(r => r.UserId == model.UserId && r.CourseId == model.CourseId);

            if (model.UserId != Guid.Empty)
            {
                await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == model.UserId);
            }

            if (model.UserId != Guid.Empty)
            {
                await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.CourseId);
            }

            var rating = await _ratingRepository.GetById(model.Id);

            rating!.Value = model.Value ?? rating.Value;
            rating.CourseId = model.CourseId != Guid.Empty ? model.CourseId : rating.CourseId;
            rating.UserId = model.UserId != Guid.Empty ? model.UserId : rating.UserId;

            await _ratingRepository.Update(rating);
        }

        private FilterOptions<Rating>? MapFilterOptions(RatingFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Rating>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Value is not null)
            {
                predicate = predicate.And(r => r.Value == filter.Value);
            }

            if (filter.CourseId != Guid.Empty)
            {
                predicate = predicate.And(r => r.CourseId == filter.CourseId);
            }

            if (filter.UserId != Guid.Empty)
            {
                predicate = predicate.And(r => r.UserId == filter.UserId);
            }

            return new FilterOptions<Rating>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Rating>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                RatingOrderQueryConstants.ValueAsc => new OrderOptions<Rating>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = r => r.Value
                },
                RatingOrderQueryConstants.ValueDesc => new OrderOptions<Rating>
                {
                    Type = OrderType.Descending,
                    FieldSelector = r => r.Value
                },
                _ => null
            };
        }
    }
}
