using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Models;
using ECourses.Data.Common;
using ECourses.Data.Common.Interfaces;
using ECourses.Data.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECourses.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IPasswordService _passwordService;
        private readonly IUserDataService _userDataService;
        private readonly IRoleDataService _roleDataService;
        private readonly IEntityValidator<User> _entityValidator;
        private readonly IUserConverter _userConverter;

        public UserService(IOptions<JwtOptions> jwtOptions,
            IPasswordService passwordService,
            IUserDataService userDataService,
            IRoleDataService roleDataService,
            IEntityValidator<User> entityValidator,
            IUserConverter userConverter)
        {
            _jwtOptions = jwtOptions.Value;
            _passwordService = passwordService;
            _userDataService = userDataService;
            _roleDataService = roleDataService;
            _entityValidator = entityValidator;
            _userConverter = userConverter;
        }

        public async Task AddRoleToUser(Role role, Guid userId)
        {
            await _userDataService.AddRoleToUser(role, userId);
        }

        public async Task Create(User user, string password)
        {
            await _userDataService.Create(user, password);
        }

        public async Task Create(string roleName)
        {
            await _roleDataService.Create(roleName);
        }

        public async Task<IEnumerable<Role>> GetAllUserRoles(Guid userId)
        {
            return await _userDataService.GetAllUserRoles(userId);
        }

        public async Task<PagedList<User>> GetAllUsersInRole(string roleName)
        {
            return await _userDataService.GetAllUsersInRole(roleName);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _roleDataService.GetRoleByName(roleName);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userDataService.GetUserByEmail(email);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _userDataService.GetUserById(id);
        }

        public async Task<bool> IsRoleExists(string roleName)
        {
            return await _roleDataService.IsRoleExists(roleName);
        }

        public async Task<IdentityModel?> Login(string email, string password)
        {
            await _entityValidator.ValidateIfEntityNotFoundByCondition(u => u.Email == email);

            var user = await _userDataService.GetUserByEmail(email);

            if (user is null)
            {
                return null;
            }

            var hashedPassword = new HashedPasswordModel(user.PasswordHash, user.PasswordSalt);

            var isPasswordCorrect = _passwordService.VerifyPassword(password, hashedPassword);

            if (isPasswordCorrect)
            {
                var userRoles = await _userDataService.GetAllUserRoles(user.Id);
                var userRolesClaim = userRoles.Any() ? new Claim(ClaimTypes.Role, string.Join(", ", userRoles.Select(r => r.Name))) : null;


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"User"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                if (userRolesClaim is not null)
                {
                    claims.Add(userRolesClaim);
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new IdentityModel(tokenHandler.WriteToken(token));
            }

            return null;
        }

        public async Task Register(string email, string password, string roleName)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Username = email,
            };

            var role = await _roleDataService.GetRoleByName(roleName);

            await _userDataService.Create(user, password);
            await _userDataService.AddRoleToUser(role, user.Id);
        }
    }
}
