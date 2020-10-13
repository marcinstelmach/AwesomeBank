namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IGroupsRepository
    {
        Task<Group> FindAsync(string normalizedName);
    }
}