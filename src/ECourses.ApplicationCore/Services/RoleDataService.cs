using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.Data;
using ECourses.Data.Common.Interfaces;
using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECourses.ApplicationCore.Services
{
    public class RoleDataService : IRoleDataService
    {
        private readonly ECoursesDbContext _context;
        private readonly IEntityValidator<Role> _entityValidator;

        public RoleDataService(ECoursesDbContext context, IEntityValidator<Role> entityValidator)
        {
            _context = context;
            _entityValidator = entityValidator;
        }

        public async Task Create(string roleName)
        {
            await _entityValidator.ValidateIfEntityExistsByCondition(r => r.Name == roleName);

            _context.Roles.Add(new Role { Name = roleName });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsRoleExists(string roleName)
        {
            return await _context.Roles.AsNoTracking().AnyAsync(r => r.Name == roleName);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            await _entityValidator.ValidateIfEntityNotFoundByCondition(r => r.Name == roleName);

            var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == roleName);

            return role!;
        }
    }
}
