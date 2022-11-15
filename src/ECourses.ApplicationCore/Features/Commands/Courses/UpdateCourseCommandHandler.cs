using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands;
using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Courses
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseValidator _courseValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IEntityValidator<Category> _categoryEntityValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly ICourseConverter _courseConverter;
        private readonly IRabbitMQService _rabbitMQService;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, ICourseValidator courseValidator, 
            IEntityValidator<Course> courseEntityValidator, 
            IEntityValidator<Category> categoryEntityValidator, 
            IEntityValidator<Author> authorEntityValidator, 
            ICourseConverter courseConverter,
            IRabbitMQService rabbitMQService)
        {
            _courseRepository = courseRepository;
            _courseValidator = courseValidator;
            _courseEntityValidator = courseEntityValidator;
            _categoryEntityValidator = categoryEntityValidator;
            _authorEntityValidator = authorEntityValidator;
            _courseConverter = courseConverter;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            _courseValidator.ValidateUpdateCourseCommand(request);
            await _courseEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);
            await _courseEntityValidator.ValidateIfEntityExistsByCondition(c => c.Title == request.Title);

            if (request.CategoryId != Guid.Empty)
            {
                await _categoryEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.CategoryId);
            }

            if (request.AuthorId != Guid.Empty)
            {
                await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.AuthorId);
            }

            var course = _courseConverter.ConvertToCourse(request);

            await _courseRepository.Update(course);

            var loggingMessage = new CommandLoggingMessage<UpdateCourseCommand>(request, CommandType.Update, DateTime.Now);

            _rabbitMQService.SendMessage(loggingMessage);

            return await Task.FromResult(Unit.Value);
        }
    }
}
