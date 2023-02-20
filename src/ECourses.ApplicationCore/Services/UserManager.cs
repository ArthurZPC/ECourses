using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common;

namespace ECourses.ApplicationCore.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IRoleConverter _roleConverter;
        private readonly IUserConverter _userConverter;

        public UserManager(IUserService userService,
            IRoleConverter roleConverter,
            IUserConverter userConverter)
        {
            _userService = userService;
            _roleConverter = roleConverter;
            _userConverter = userConverter;
        }

        public async Task AddRoleToUser(RoleViewModel roleModel, Guid userId)
        {
            var role = await _userService.GetRoleByName(roleModel.Name);

            await _userService.AddRoleToUser(role, userId);
        }

        public async Task Create(UserViewModel userModel, string password)
        {
            var user = _userConverter.ConvertToUser(userModel);

            await _userService.Create(user, password);
        }

        public async Task Create(string roleName)
        {
            await _userService.Create(roleName);
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllUserRoles(Guid userId)
        {
            var roles = await _userService.GetAllUserRoles(userId);

            return roles.Select(x => _roleConverter.ConvertToViewModel(x));
        }

        public async Task<PagedList<UserViewModel>> GetAllUsersInRole(string roleName)
        {
            var users = await _userService.GetAllUsersInRole(roleName);

            return new PagedList<UserViewModel>
            {
                Count = users.Count,
                Items = users.Items.Select(x => _userConverter.ConvertToViewModel(x))
            };
        }

        public async Task<RoleViewModel> GetRoleByName(string roleName)
        {
            var role = await _userService.GetRoleByName(roleName);
            return _roleConverter.ConvertToViewModel(role);
        }

        public async Task<UserViewModel?> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            return _userConverter.ConvertToViewModel(user!);
        }

        public async Task<UserViewModel?> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);

            return _userConverter.ConvertToViewModel(user!);
        }

        public async Task<bool> IsRoleExists(string roleName)
        {
            return await _userService.IsRoleExists(roleName);
        }

        public async Task<IdentityModel?> Login(string email, string password)
        {
            return await _userService.Login(email, password);
        }

        public async Task Register(string email, string password, string role)
        {
            await _userService.Register(email, password, role);
        }
    }
}
