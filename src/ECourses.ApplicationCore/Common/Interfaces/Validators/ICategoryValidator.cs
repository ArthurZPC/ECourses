using ECourses.ApplicationCore.Features.Commands.Categories;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface ICategoryValidator
    {
        void ValidateCreateCategoryCommand(CreateCategoryCommand command);
        void ValidateUpdateCategoryCommand(UpdateCategoryCommand command);
    }
}
