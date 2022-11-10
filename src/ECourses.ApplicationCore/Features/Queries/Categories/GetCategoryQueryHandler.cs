using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Categories
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private IEntityValidator<Category> _entityValidator;
        private ICategoryConverter _categoryConverter;

        public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IEntityValidator<Category> entityValidator, 
            ICategoryConverter categoryConverter)
        {
            _categoryRepository = categoryRepository;
            _entityValidator = entityValidator;
            _categoryConverter = categoryConverter;
        }

        public async Task<CategoryViewModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == request.Id);

            return _categoryConverter.ConvertToViewModel(category!);
        }
    }
}
