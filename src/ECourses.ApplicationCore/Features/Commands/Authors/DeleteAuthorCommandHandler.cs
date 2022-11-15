using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IEntityValidator<Author> _entityValidator;
        private readonly IRabbitMQService _rabbitMQService;
        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IEntityValidator<Author> entityValidator, IRabbitMQService rabbitMQService)
        {
            _authorRepository = authorRepository;
            _entityValidator = entityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.Id);

            await _authorRepository.Delete(request.Id);


            var loggingMessage = new CommandLoggingMessage<DeleteAuthorCommand>(request, CommandType.Delete, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
