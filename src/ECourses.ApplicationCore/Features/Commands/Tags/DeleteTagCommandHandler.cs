using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IEntityValidator<Tag> _entityValidator;
        private readonly IRabbitMQService _rabbitMQService;

        public DeleteTagCommandHandler(ITagRepository tagRepository, IEntityValidator<Tag> entityValidator,
            IRabbitMQService rabbitMQService)
        {
            _tagRepository = tagRepository;
            _entityValidator = entityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(t => t.Id == request.Id);

            await _tagRepository.Delete(request.Id);

            var loggingMessage = new CommandLoggingMessage<DeleteTagCommand>(request, CommandType.Delete, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
