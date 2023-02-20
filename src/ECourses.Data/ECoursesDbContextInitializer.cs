using ECourses.Data.Common.Exceptions;
using ECourses.Data.Common.Interfaces;
using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data
{
    public class ECoursesDbContextInitializer
    {
        private readonly ECoursesDbContext _context;
        private readonly IUserDataService _userDataService;
        private readonly IRoleDataService _roleDataService;

        public ECoursesDbContextInitializer(ECoursesDbContext context,
            IUserDataService userDataService, 
            IRoleDataService roleDataService)
        {
            _context = context;
            _userDataService = userDataService;
            _roleDataService = roleDataService;
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

            if (!await _roleDataService.IsRoleExists(administratorRole.Name))
            {
                await _roleDataService.Create(administratorRole.Name);
            }

            var defaultUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = "Administrator",
                Email = "admin@admin.com",
            };

            var administrators = await _userDataService.GetAllUsersInRole(administratorRole.Name);

            if (administrators.Count == 0)
            {
                await _userDataService.Create(defaultUser, "admin!");
                await _userDataService.AddRoleToUser(administratorRole, defaultUser.Id);
            }
        }
    }
}
