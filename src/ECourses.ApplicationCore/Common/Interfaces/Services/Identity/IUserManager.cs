using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Common;

namespace ECourses.ApplicationCore.Common.Interfaces.Services.Identity
{
    public interface IUserManager
    {
        public Task<PagedList<UserViewModel>> GetAllUsersInRole(string roleName);

        public Task<UserViewModel?> GetUserById(Guid id);

        public Task<UserViewModel?> GetUserByEmail(string email);

        public Task Create(UserViewModel user, string password);

        public Task AddRoleToUser(RoleViewModel role, Guid userId);

        public Task<IEnumerable<RoleViewModel>> GetAllUserRoles(Guid userId);

        public Task<bool> IsRoleExists(string roleName);

        public Task Create(string roleName);

        Task<RoleViewModel> GetRoleByName(string roleName);
        Task Register(string email, string password, string role);

        Task<IdentityModel?> Login(string email, string password);
    }
}
