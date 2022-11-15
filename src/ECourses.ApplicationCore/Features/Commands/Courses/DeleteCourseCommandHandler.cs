using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Courses
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEntityValidator<Course> _entityValidator;
        private readonly IRabbitMQService _rabbitMQService;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository, IEntityValidator<Course> entityValidator,
            IRabbitMQService rabbitMQService)
        {
            _courseRepository = courseRepository;
            _entityValidator = entityValidator;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            await _courseRepository.Delete(request.Id);

            var loggingMessage = new CommandLoggingMessage<DeleteCourseCommand>(request, CommandType.Delete, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
