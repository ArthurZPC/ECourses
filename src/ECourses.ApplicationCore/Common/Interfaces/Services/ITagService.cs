using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAllTags();
        Task<TagViewModel> GetTagById(Guid id);
        Task Create(CreateTagViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateTagViewModel model);
        Task<PagedListModel<TagViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, TagFilterQuery? filter);
    }
}
