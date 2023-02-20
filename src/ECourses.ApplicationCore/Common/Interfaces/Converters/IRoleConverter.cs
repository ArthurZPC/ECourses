using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IRoleConverter
    {
        Role ConvertToRole(RoleViewModel model);
        RoleViewModel ConvertToViewModel(Role role);
    }
}
