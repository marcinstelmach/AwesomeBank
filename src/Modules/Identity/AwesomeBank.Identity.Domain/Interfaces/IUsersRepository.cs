namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IUsersRepository : IRepository
    {
        Task<bool> ExistsUserAsync(string email);

        void AddUser(User user);
    }
}