using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
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
        private readonly IRabbitMQService _rabbitMQService;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository, IRatingValidator ratingValidator, 
            IEntityValidator<Rating> ratingEntityValidator, 
            IEntityValidator<User> userEntityValidator, 
            IEntityValidator<Course> courseValidator, 
            IRatingConverter ratingConverter,
            IRabbitMQService rabbitMQService)
        {
            _ratingRepository = ratingRepository;
            _ratingValidator = ratingValidator;
            _ratingEntityValidator = ratingEntityValidator;
            _userEntityValidator = userEntityValidator;
            _courseEntityValidator = courseValidator;
            _ratingConverter = ratingConverter;
            _rabbitMQService = rabbitMQService;
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

            var loggingMessage = new CommandLoggingMessage<UpdateRatingCommand>(request, CommandType.Create, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
