using ECourses.ApplicationCore.Common.Interfaces.Services.Identity;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data;
using ECourses.Data.Common;
using ECourses.Data.Common.Interfaces;
using ECourses.Data.Extensions;
using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECourses.ApplicationCore.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly ECoursesDbContext _context;
        private readonly IEntityValidator<User> _userEntityValidator;
        private readonly IEntityValidator<Role> _roleEntityValidator;
        private readonly IPasswordService _passwordService;

        public UserDataService(ECoursesDbContext context, 
            IEntityValidator<User> entityValidator, 
            IEntityValidator<Role> roleEntityValidator,
            IPasswordService passwordService)
        {
            _context = context;
            _userEntityValidator = entityValidator;
            _roleEntityValidator = roleEntityValidator;
            _passwordService = passwordService;
        }
        public async Task AddRoleToUser(Role role, Guid userId)
        {
            await _roleEntityValidator.ValidateIfEntityNotFoundByCondition(r => r.Id == role.Id);
            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == userId);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            user!.Roles.Add(role);

            await _context.SaveChangesAsync();
        }

        public async Task Create(User user, string password)
        {
            await _userEntityValidator.ValidateIfEntityExistsByCondition(u => u.Id == user.Id || u.Email == user.Email);

            var hashedPassword = _passwordService.HashPassword(password);

            user.PasswordHash = hashedPassword.PasswordHash;
            user.PasswordSalt = hashedPassword.PasswordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllUserRoles(Guid userId)
        {
            await _userEntityValidator.ValidateIfEntityNotFoundByCondition(u => u.Id == userId);

            var user = await _context.Users
                .Include(u => u.Roles)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user!.Roles;
        }

        public async Task<PagedList<User>> GetAllUsersInRole(string roleName)
        {
            return await _context.Users
                .Include(u => u.Ratings)
                .Include(u => u.Roles)
                .AsNoTracking()
                .ToPagedListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.Ratings)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users
                .Include(u => u.Ratings)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
