using ECourses.ApplicationCore.Interfaces.Converters;
using ECourses.ApplicationCore.Interfaces.Services;
using ECourses.ApplicationCore.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Common.Interfaces.Repositories;

namespace ECourses.ApplicationCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryConverter _categoryConverter;
        private readonly ICategoryValidator _categoryValidator;

        public CategoryService(ICategoryRepository categoryRepository,
            ICategoryConverter categoryConverter,
            ICategoryValidator categoryValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryConverter = categoryConverter;
            _categoryValidator = categoryValidator;
        }

        public async Task Create(CreateCategoryViewModel model)
        {
            await _categoryValidator.ValidateCreateCategoryViewModel(model);

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Create(category);
        }

        public async Task Delete(Guid id)
        {
            await _categoryValidator.ValidateIfCategoryFound(id);

            await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Select(c => _categoryConverter.ConvertToViewModel(c)).ToList();
        }

        public async Task<CategoryViewModel?> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            return category == null ? null : _categoryConverter.ConvertToViewModel(category);
        }

        public async Task Update(UpdateCategoryViewModel model)
        {
            await _categoryValidator.ValidateUpdateCategoryViewModel(model);

            var category = _categoryConverter.ConvertToCategory(model);

            await _categoryRepository.Update(category);
        }
    }
}
