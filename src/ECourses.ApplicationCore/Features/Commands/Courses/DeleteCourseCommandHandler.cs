using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Courses
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEntityValidator<Course> _entityValidator;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository, IEntityValidator<Course> entityValidator)
        {
            _courseRepository = courseRepository;
            _entityValidator = entityValidator;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            await _courseRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }
    }
}
