using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(Guid id);
        Task Create(CreateCategoryViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateCategoryViewModel model);
        Task<PagedListModel<CategoryViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, CategoryFilterQuery? filter);

    }
}
