using ECourses.ApplicationCore.Models;

namespace ECourses.ApplicationCore.Common.Interfaces.Services.Identity
{
    public interface IPasswordService
    {
        public HashedPasswordModel HashPassword(string password);
        public bool VerifyPassword(string password, HashedPasswordModel storedHashedPassword);
    }
}
