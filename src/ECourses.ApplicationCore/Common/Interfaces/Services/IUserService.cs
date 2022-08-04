using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task Register(string email, string password, string role);

        Task<IdentityModel?> Login(string email, string password);

        Task<IEnumerable<UserViewModel>> GetAllUsersInRole(string role);

        Task<UserViewModel?> GetUserById(Guid id);
    }
}
