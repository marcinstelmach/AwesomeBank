namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IUserClaimsRepository
    {
        Task<IEnumerable<Claim>> GetUserClaimsAsync(UserId userId);
    }
}