using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Identity;

namespace ECourses.ApplicationCore.Converters
{
    public class RoleConverter : IRoleConverter
    {
        public Role ConvertToRole(RoleViewModel model)
        {
            return new Role()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public RoleViewModel ConvertToViewModel(Role role)
        {
            return new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
