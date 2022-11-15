using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IEntityValidator<Rating> _entityValidator;
        private readonly IRabbitMQService _rabbitMQService;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository, IEntityValidator<Rating> entityValidator,
            IRabbitMQService rabbitMQService)
        {
            _ratingRepository = ratingRepository;
            _entityValidator = entityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _ratingRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == request.Id);

            await _ratingRepository.Delete(request.Id);

            var loggingMessage = new CommandLoggingMessage<DeleteRatingCommand>(request, CommandType.Delete, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
