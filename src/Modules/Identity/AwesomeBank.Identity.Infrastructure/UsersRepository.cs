namespace AwesomeBank.Identity.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UsersRepository : IUsersRepository
    {
        private readonly IdentityContext _identityContext;

        public UsersRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var users = await _identityContext.Users.ToArrayAsync();
            return users;
        }
    }
}