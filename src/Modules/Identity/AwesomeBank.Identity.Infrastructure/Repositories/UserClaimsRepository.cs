namespace AwesomeBank.Identity.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserClaimsRepository : IUserClaimsRepository
    {
        private readonly IdentityContext _context;

        public UserClaimsRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(UserId userId)
        {
            var claims = await _context.Users
                .Include(x => x.ApplicationUserGroups)
                .ThenInclude(x => x.ApplicationGroup)
                .ThenInclude(x => x.ApplicationGroupClaims)
                .ThenInclude(x => x.Claim)
                .Where(x => x.Id == userId)
                .SelectMany(x => x.ApplicationUserGroups)
                .Select(x => x.ApplicationGroup)
                .SelectMany(x => x.ApplicationGroupClaims)
                .Select(x => x.Claim)
                .ToArrayAsync();

            return claims;
        }
    }
}