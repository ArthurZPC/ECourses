using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Converters
{
    public class UserConverter : IUserConverter
    {
        public User ConvertToUser(UserViewModel model)
        {
            return new User()
            {
                Id = model.Id,
                Email = model.Email,
                Username = model.Username,
            };
        }

        public UserViewModel ConvertToViewModel(User user)
        {
            var roles = user.Roles?.Select(r => r.Name);

            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username!,
                Roles = roles is not null ? string.Join(", ", roles!) : string.Empty,
            };
        }
    }
}
