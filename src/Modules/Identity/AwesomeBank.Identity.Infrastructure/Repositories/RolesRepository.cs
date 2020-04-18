namespace AwesomeBank.Identity.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RolesRepository : IRolesRepository
    {
        private readonly IdentityContext _context;

        public RolesRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}