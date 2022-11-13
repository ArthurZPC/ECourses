using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
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

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, ICourseValidator courseValidator, 
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

            return await Task.FromResult(Unit.Value);
        }
    }
}
