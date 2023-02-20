using ECourses.Data.Identity;

namespace ECourses.Data.Common.Interfaces
{
    public interface IUserDataService
    {
        public Task<PagedList<User>> GetAllUsersInRole(string roleName);

        public Task<User?> GetUserById(Guid id);

        public Task<User?> GetUserByEmail(string email);

        public Task Create(User user, string password);

        public Task AddRoleToUser(Role role, Guid userId);

        public Task<IEnumerable<Role>> GetAllUserRoles(Guid userId);
    }
}
