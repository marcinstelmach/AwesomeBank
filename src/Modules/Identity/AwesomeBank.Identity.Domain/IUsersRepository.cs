namespace AwesomeBank.Identity.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAsync();
    }
}