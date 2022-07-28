using ECourses.Data.Common.Exceptions;
using ECourses.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data
{
    public class ECoursesDbContextInitializer
    {
        private readonly ECoursesDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ECoursesDbContextInitializer(ECoursesDbContext context,
            UserManager<User> userManager, 
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseInitializationException(ex);
            }
        }

        public async Task Seed()
        {
            try
            {
                await SeedIdentity();
            }
            catch (Exception ex)
            {
                throw new DatabaseSeedingException(ex);
            }
        }

        private async Task SeedIdentity()
        {
            var administratorRole = new Role
            {
                Name = "Administrator"
            };

            if (!await _roleManager.RoleExistsAsync(administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            var defaultUser = new User()
            {
                UserName = "Administrator",
                Email = "admin@admin.com",
            };

            var administrators = await _userManager.GetUsersInRoleAsync(administratorRole.Name);

            if (administrators.Count == 0)
            {
                await _userManager.CreateAsync(defaultUser, "admin!");
                await _userManager.AddToRoleAsync(defaultUser, administratorRole.Name);
            }
        }
    }
}
