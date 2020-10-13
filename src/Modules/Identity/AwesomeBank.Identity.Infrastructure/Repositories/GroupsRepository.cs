namespace AwesomeBank.Identity.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GroupsRepository : IGroupsRepository
    {
        private readonly IdentityContext _context;

        public GroupsRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Group> FindAsync(string normalizedName)
        {
            return await _context.Groups.FirstOrDefaultAsync(x => x.NormalizedName == normalizedName);
        }
    }
}