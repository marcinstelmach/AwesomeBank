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

        public async Task<IEnumerable<string>> GetUserClaimsAsync(UserId userId)
        {
            return await _context.Users
                .Where(x => x.Id == userId)
                .SelectMany(x => x.UserGroups)
                .Select(x => x.Group)
                .SelectMany(x => x.GroupClaims)
                .Select(x => x.ClaimValue)
                .ToArrayAsync();
        }
    }
}