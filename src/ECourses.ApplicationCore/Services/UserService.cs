using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(IOptions<JwtOptions> jwtOptions, 
            SignInManager<User> signInManager, 
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersInRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            return users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Role = role,
            });
        }

        public async Task<UserViewModel?> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var userRole = await _userManager.GetRolesAsync(user);

            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = userRole.FirstOrDefault()!
            };
        }

        public async Task<IdentityModel?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, $"User"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, userRole.FirstOrDefault()!),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new IdentityModel
                {
                    Token = tokenHandler.WriteToken(token)
                };
            }

            return null;
        }

        public async Task Register(string email, string password, string role)
        {
            var user = new User
            {
                Email = email,
                UserName = email,
            };

            var result1 = await _userManager.CreateAsync(user, password);
            var result2 = await _userManager.AddToRoleAsync(user, role);
        }
    }
}
