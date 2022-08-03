using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.ApplicationCore.WebQueries;
using ECourses.ApplicationCore.WebQueries.Filters;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAuthors();
        Task<AuthorViewModel> GetAuthorById(Guid id);
        Task Create(CreateAuthorViewModel model);
        Task Delete(Guid id);
        Task Update(UpdateAuthorViewModel model);
        Task<PagedListModel<AuthorViewModel>> GetPagedList(PaginationQuery pagination, string? orderField, AuthorFilterQuery? filter);
    }
}
