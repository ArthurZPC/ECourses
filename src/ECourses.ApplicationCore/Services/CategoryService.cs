using ECourses.ApplicationCore.Interfaces.Converters;
using ECourses.ApplicationCore.Interfaces.Services;
using ECourses.ApplicationCore.Interfaces.Validators;
using ECourses.ApplicationCore.Interfaces.Validators.Common;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryConverter _categoryConverter;
        private readonly ICategoryValidator _categoryValidator;
        private readonly IEntityValidator<Category> _entityValidator;

        public CategoryService(ICategoryRepository categoryRepository,
            ICategoryConverter categoryConverter,
            ICategoryValidator categoryValidator,
            IEntityValidator<Category> entityValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryConverter = categoryConverter;
            _categoryValidator = categoryValidator;
            _entityValidator = entityValidator;
        }

        public async Task Create(CreateCategoryViewModel model)
        {
            _categoryValidator.ValidateCreateCategoryViewModel(model);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == model.Title.ToLower());

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Create(category);
        }

        public async Task Delete(Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Select(c => _categoryConverter.ConvertToViewModel(c)).ToList();
        }

        public async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == id);

            return _categoryConverter.ConvertToViewModel(category!);
        }

        public async Task Update(UpdateCategoryViewModel model)
        {
            _categoryValidator.ValidateUpdateCategoryViewModel(model);
            await _entityValidator.ValidateIfEntityNotFoundByCondition(c => c.Id == model.Id);
            await _entityValidator.ValidateIfEntityExistsByCondition(c => c.Title.ToLower() == model.Title.ToLower());      

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Update(category);
        }
    }
}
