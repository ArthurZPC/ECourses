using ECourses.ApplicationCore.Models;
using ECourses.Data.Common.Interfaces;

namespace ECourses.ApplicationCore.Common.Interfaces.Services.Identity
{
    public interface IUserService : IUserDataService, IRoleDataService
    {
        Task Register(string email, string password, string role);

        Task<IdentityModel?> Login(string email, string password);
    }
}
