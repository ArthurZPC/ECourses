using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Courses
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseValidator _courseValidator;
        private readonly IEntityValidator<Course> _courseEntityValidator;
        private readonly IEntityValidator<Category> _categoryEntityValidator;
        private readonly IEntityValidator<Author> _authorEntityValidator;
        private readonly ICourseConverter _courseConverter;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseValidator courseValidator, 
            IEntityValidator<Course> courseEntityValidator, 
            IEntityValidator<Category> categoryEntityValidator, 
            IEntityValidator<Author> authorEntityValidator,
            ICourseConverter courseConverter)
        {
            _courseRepository = courseRepository;
            _courseValidator = courseValidator;
            _courseEntityValidator = courseEntityValidator;
            _categoryEntityValidator = categoryEntityValidator;
            _authorEntityValidator = authorEntityValidator;
            _courseConverter = courseConverter;
        }

        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            _courseValidator.ValidateCreateCourseCommand(request);
            await _courseEntityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == request.Title.ToLower());

            await _categoryEntityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.CategoryId);
            await _authorEntityValidator.ValidateIfEntityNotFoundByCondition(a => a.Id == request.AuthorId);

            var course = _courseConverter.ConvertToCourse(request);

            await _courseRepository.Create(course);

            return await Task.FromResult(Unit.Value);
        }
    }
}
