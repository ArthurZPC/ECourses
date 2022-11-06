using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CourseViewModel> GetCourseById(Guid id);
        Task Create(CreateCourseViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateCourseViewModel model);
        Task<PagedListModel<CourseViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, CourseFilterQuery? filter);
    }
}
