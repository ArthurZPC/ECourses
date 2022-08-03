using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class AuthorConverter : IAuthorConverter
    {
        public Author ConvertToAuthor(AuthorViewModel model)
        {
            return new Author
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId,
            };
        }

        public Author ConvertToAuthor(CreateAuthorViewModel model)
        {
            return new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId,
            };
        }

        public Author ConvertToAuthor(UpdateAuthorViewModel model)
        {
            return new Author
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId,
            };
        }

        public AuthorViewModel ConvertToViewModel(Author model)
        {
            return new AuthorViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId,
            };
        }
    }
}
