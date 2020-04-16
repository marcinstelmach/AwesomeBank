namespace AwesomeBank.Identity.Infrastructure
{
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Domain.Specifications;
    using AwesomeBank.BuildingBlocks.Infrastructure;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UsersRepository : Repository, IUsersRepository
    {
        private readonly IdentityContext _identityContext;

        public UsersRepository(IdentityContext identityContext)
            : base(identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<bool> ExistsUserAsync(string email)
        {
            return await _identityContext.Users
                .AnyAsync(x => x.Email == email.ToLowerInvariant());
        }

        public async Task<User> FindUserAsync(Specification<User> specification)
        {
            return await _identityContext.Users.FirstOrDefaultAsync(specification.ToExpression());
        }

        public void AddUser(User user)
        {
            _identityContext.Users.Add(user);
        }
    }
}