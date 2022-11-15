using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagValidator _tagValidator;
        private readonly IEntityValidator<Tag> _entityValidator;
        private readonly ITagConverter _tagConverter;
        private readonly IRabbitMQService _rabbitMQService;

        public CreateTagCommandHandler(ITagRepository tagRepository, ITagValidator tagValidator, 
            IEntityValidator<Tag> entityValidator, 
            ITagConverter tagConverter,
            IRabbitMQService rabbitMQService)
        {
            _tagRepository = tagRepository;
            _tagValidator = tagValidator;
            _entityValidator = entityValidator;
            _tagConverter = tagConverter;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            _tagValidator.ValidateCreateTagCommand(request);
            await _entityValidator.ValidateIfEntityExistsByCondition(t => t.Title.ToLower() == request.Title.ToLower());

            var tag = _tagConverter.ConvertToTag(request);

            await _tagRepository.Create(tag);

            var loggingMessage = new CommandLoggingMessage<CreateTagCommand>(request, CommandType.Create, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
