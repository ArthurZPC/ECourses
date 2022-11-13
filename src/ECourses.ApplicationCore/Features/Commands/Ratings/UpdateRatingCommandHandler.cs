using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using ECourses.Data.Identity;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingValidator _ratingValidator;
        private readonly IEntityValidator<Rating> _ratingEntityValidator;
        private readonly IEntityValidator<User> _userEntityValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IRatingConverter _ratingConverter;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository, IRatingValidator ratingValidator, 
            IEntityValidator<Rating> ratingEntityValidator, 
            IEntityValidator<User> userEntityValidator, 
            IEntityValidator<Course> courseValidator, 
            IRatingConverter ratingConverter)
        {
            _ratingRepository = ratingRepository;
            _ratingValidator = ratingValidator;
            _ratingEntityValidator = ratingEntityValidator;
            _userEntityValidator = userEntityValidator;
            _courseEntityValidator = courseValidator;
            _ratingConverter = ratingConverter;
        }

        public async Task<Unit> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            _ratingValidator.ValidateUpdateRatingCommand(request);
            await _ratingEntityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == request.Id);

            if (request.UserId != Guid.Empty)
            {
                await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == request.UserId);
            }

            if (request.UserId != Guid.Empty)
            {
                await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.CourseId);
            }

            var rating = _ratingConverter.ConvertToRating(request);

            await _ratingRepository.Update(rating);

            return await Task.FromResult(Unit.Value);
        }
    }
}
