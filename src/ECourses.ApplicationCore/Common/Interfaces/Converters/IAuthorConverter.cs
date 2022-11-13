using ECourses.ApplicationCore.Features.Commands.Authors;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IAuthorConverter
    {
        Author ConvertToAuthor(AuthorViewModel model);
        Author ConvertToAuthor(CreateAuthorCommand command);
        Author ConvertToAuthor(UpdateAuthorCommand command);
        AuthorViewModel ConvertToViewModel(Author model);
    }
}
