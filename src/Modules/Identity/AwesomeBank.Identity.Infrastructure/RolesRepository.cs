namespace AwesomeBank.Identity.Infrastructure
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Infrastructure.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class RolesRepository : IRolesRepository
    {
        private readonly IdentityContext _context;

        public RolesRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleAndEnsureExistsAsync(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
            if (role == null)
            {
                throw new RoleNotFoundException(name);
            }

            return role;
        }
    }
}