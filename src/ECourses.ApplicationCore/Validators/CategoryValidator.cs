using ECourses.ApplicationCore.Exceptions;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Common.Interfaces.Repositories;

namespace ECourses.ApplicationCore.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task ValidateCreateCategoryViewModel(CreateCategoryViewModel model)
        {
            if (model is null) 
            {
                throw new ArgumentNullException(nameof(model), Resources.Common.Validation_Null);
            }

            if (string.IsNullOrEmpty(model.Title))
            {            
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(model.Title));
            }

            var isCategoryExists = await IsCategoryExists(model.Title);

            if (isCategoryExists) 
            {
                throw new EntityExistsException(Resources.Category.Validation_Exists.F(model.Title));
            }
        }

        public async Task ValidateUpdateCategoryViewModel(UpdateCategoryViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model), Resources.Common.Validation_Null);
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(model.Title));
            }

            var isCategoryExists = await IsCategoryExists(model.Title);

            if (isCategoryExists)
            {
                throw new EntityExistsException(Resources.Category.Validation_Exists.F(model.Title));
            }
        }

        public async Task ValidateIfCategoryFound(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            
            if (category is null)
            {
                throw new EntityNotFoundException(Resources.Category.Validation_NotFound.F(id));
            }
        }

        private async Task<bool> IsCategoryExists(string title)
        {
            var category = await _categoryRepository.GetByCondition(c => c.Title.ToLower() == title.ToLower());

            return category is not null;
        }
    }
}
