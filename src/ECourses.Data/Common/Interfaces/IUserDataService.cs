using ECourses.Data.Identity;

namespace ECourses.Data.Common.Interfaces
{
    public interface IUserDataService
    {
        public Task<IEnumerable<User>> GetAllUsersInRole(string roleName);

        public Task<User> GetUserById(Guid id);

        public Task<User> GetUserByEmail(string email);

        public Task Create(User user, string password);

        public Task AddRoleToUser(Role role, Guid userId);
    }
}
