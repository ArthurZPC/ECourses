using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IAuthorConverter
    {
        Author ConvertToAuthor(AuthorViewModel model);
        Author ConvertToAuthor(CreateAuthorViewModel model);
        Author ConvertToAuthor(UpdateAuthorViewModel model);
        AuthorViewModel ConvertToViewModel(Author model);
    }
}
