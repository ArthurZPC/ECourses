using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IUserConverter
    {
        User ConvertToUser(UserViewModel model);
        UserViewModel ConvertToViewModel(User user);
    }
}
