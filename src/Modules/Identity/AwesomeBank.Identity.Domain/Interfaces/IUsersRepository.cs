namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IUsersRepository : IRepository
    {
        Task<IEnumerable<User>> GetAsync();

        void AddUser(User user);
    }
}