using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using ECourses.Data.Identity;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingValidator _ratingValidator;
        private readonly IEntityValidator<Rating> _ratingEntityValidator;
        private readonly IEntityValidator<User> _userEntityValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IRatingConverter _ratingConverter;

        public CreateRatingCommandHandler(IRatingRepository ratingRepository, IRatingValidator ratingValidator, 
            IEntityValidator<Rating> ratingEntityValidator, 
            IEntityValidator<User> userEntityValidator, 
            IEntityValidator<Course> courseEntityValidator, 
            IRatingConverter ratingConverter)
        {
            _ratingRepository = ratingRepository;
            _ratingValidator = ratingValidator;
            _ratingEntityValidator = ratingEntityValidator;
            _userEntityValidator = userEntityValidator;
            _courseEntityValidator = courseEntityValidator;
            _ratingConverter = ratingConverter;
        }

        public async Task<Unit> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            _ratingValidator.ValidateCreateRatingCommand(request);
            await _ratingEntityValidator.ValidateIfEntityExistsByCondition(r => r.UserId == request.UserId && r.CourseId == request.CourseId);

            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == request.UserId);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.CourseId);

            var rating = _ratingConverter.ConvertToRating(request);

            await _ratingRepository.Create(rating);

            return await Task.FromResult(Unit.Value);
        }
    }
}
