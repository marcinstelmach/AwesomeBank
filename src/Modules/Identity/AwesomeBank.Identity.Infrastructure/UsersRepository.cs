namespace AwesomeBank.Identity.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Infrastructure;
    using AwesomeBank.Identity.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UsersRepository : Repository, IUsersRepository
    {
        private readonly IdentityContext _identityContext;

        public UsersRepository(IdentityContext identityContext)
            : base(identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            var users = await _identityContext.Users.ToArrayAsync();
            return users;
        }

        public void AddUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}