using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel?> GetCategoryById(Guid id);
        Task Create(CreateCategoryViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateCategoryViewModel model);

    }
}
