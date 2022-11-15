using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEntityValidator<Category> _entityValidator;
        private readonly IRabbitMQService _rabbitMQService;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IEntityValidator<Category> entityValidator,
            IRabbitMQService rabbitMQService)
        {
            _categoryRepository = categoryRepository;
            _entityValidator = entityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            await _categoryRepository.Delete(request.Id);

            var loggingMessage = new CommandLoggingMessage<DeleteCategoryCommand>(request, CommandType.Delete, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
