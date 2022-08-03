using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingViewModel>> GetAllRatings();
        Task<RatingViewModel> GetRatingById(Guid id);
        Task Create(CreateRatingViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateRatingViewModel model);
        Task<PagedListModel<RatingViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, RatingFilterQuery? filter);
    }
}
