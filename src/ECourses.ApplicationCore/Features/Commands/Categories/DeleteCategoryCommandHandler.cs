using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEntityValidator<Category> _entityValidator;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IEntityValidator<Category> entityValidator)
        {
            _categoryRepository = categoryRepository;
            _entityValidator = entityValidator;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            await _categoryRepository.Delete(request.Id);

            return await Task.FromResult(Unit.Value);
        }
    }
}
