using ECourses.ApplicationCore.Exceptions;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Interfaces.Validators;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Entities;
using System.Linq.Expressions;

namespace ECourses.ApplicationCore.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        public void ValidateCreateCategoryViewModel(CreateCategoryViewModel model)
        {
            if (model is null) 
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateCategoryViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {            
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }
        }

        public void ValidateUpdateCategoryViewModel(UpdateCategoryViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model), Resources.Common.Validation_Null.F(typeof(UpdateCategoryViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }
        }
    }
}
