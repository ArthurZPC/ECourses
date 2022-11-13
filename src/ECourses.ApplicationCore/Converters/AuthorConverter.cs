using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Authors;
using ECourses.ApplicationCore.ViewModels;
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

        public Author ConvertToAuthor(CreateAuthorCommand command)
        {
            return new Author
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserId = command.UserId,
            };
        }

        public Author ConvertToAuthor(UpdateAuthorCommand command)
        {
            return new Author
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserId = command.UserId,
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
