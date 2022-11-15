using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryValidator _categoryValidator;
        private readonly IEntityValidator<Category> _entityValidator;
        private readonly ICategoryConverter _categoryConverter;
        private readonly IRabbitMQService _rabbitMQService;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryValidator categoryValidator, 
            IEntityValidator<Category> entityValidator, 
            ICategoryConverter categoryConverter,
            IRabbitMQService rabbitMQService)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
            _entityValidator = entityValidator;
            _categoryConverter = categoryConverter;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _categoryValidator.ValidateCreateCategoryCommand(request);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == request.Title.ToLower());

            var category = _categoryConverter.ConvertToCategory(request);

            await _categoryRepository.Create(category);

            var loggingMessage = new CommandLoggingMessage<CreateCategoryCommand>(request, CommandType.Create, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
