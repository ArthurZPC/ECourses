using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using ECourses.ApplicationCore.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ECourses.ApplicationCore.Services
{
    public class PasswordService : IPasswordService
    {
        private const int KeySize = 64;

        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private readonly PasswordSettingsOptions _passwordSettings;

        public PasswordService(IOptions<PasswordSettingsOptions> passwordSettings)
        {
            _passwordSettings = passwordSettings.Value;
        }
        public HashedPasswordModel HashPassword(string password)
        {
            var iterations = _passwordSettings.Iterations;

            var salt = RandomNumberGenerator.GetBytes(KeySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                KeySize);

            return new HashedPasswordModel(Convert.ToHexString(hash), Convert.ToHexString(salt));
        }

        public bool VerifyPassword(string password, HashedPasswordModel storedHashedPassword)
        {
            var iterations = _passwordSettings.Iterations;

            var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Convert.FromHexString(storedHashedPassword.PasswordSalt),
                iterations,
                hashAlgorithm,
                KeySize);

            return storedHashedPassword.PasswordHash == Convert.ToHexString(hashedPassword);
        }
    }
}
