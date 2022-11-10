using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Courses
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseViewModel>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEntityValidator<Course> _entityValidator;
        private readonly ICourseConverter _courseConverter;

        public GetCourseQueryHandler(ICourseRepository courseRepository, IEntityValidator<Course> entityValidator,
            ICourseConverter courseConverter)
        {
            _courseRepository = courseRepository;
            _entityValidator = entityValidator;
            _courseConverter = courseConverter;
        }

        public async Task<CourseViewModel> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            return _courseConverter.ConvertToViewModel(course!);
        }
    }
}
