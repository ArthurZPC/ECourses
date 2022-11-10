using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Ratings
{
    public class GetRatingQueryHandler : IRequestHandler<GetRatingQuery, RatingViewModel>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IEntityValidator<Rating> _entityValidator;
        private readonly IRatingConverter _ratingConverter;

        public GetRatingQueryHandler(IRatingRepository ratingRepository, IEntityValidator<Rating> entityValidator,
            IRatingConverter ratingConverter)
        {
            _ratingRepository = ratingRepository;
            _entityValidator = entityValidator;
            _ratingConverter = ratingConverter;
        }

        public async Task<RatingViewModel> Handle(GetRatingQuery request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == request.Id);

            return _ratingConverter.ConvertToViewModel(rating!);
        }
    }
}
