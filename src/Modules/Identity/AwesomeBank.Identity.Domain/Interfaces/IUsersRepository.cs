namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.BuildingBlocks.Domain.Specifications;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IUsersRepository : IRepository
    {
        Task<bool> ExistsUserAsync(string email);

        Task<User> FindUserAsync(Specification<User> specification);

        void AddUser(User user);
    }
}