using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface IVideoService
    {
        Task<VideoViewModel> GetVideoById(Guid id);
        Task Create(CreateVideoViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateVideoViewModel model);
        Task<PagedListModel<VideoViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, VideoFilterQuery? filter);
    }
}
