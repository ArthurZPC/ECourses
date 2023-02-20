using ECourses.Data.Identity;

namespace ECourses.Data.Common.Interfaces
{
    public interface IRoleDataService
    {
        public Task<bool> IsRoleExists(string roleName);

        public Task Create(string roleName);

        Task<Role> GetRoleByName(string roleName);
    }
}
