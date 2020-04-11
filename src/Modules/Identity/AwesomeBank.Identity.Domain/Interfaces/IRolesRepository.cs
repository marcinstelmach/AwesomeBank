namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;

    public interface IRolesRepository
    {
        Task<Role> GetRoleAndEnsureExistsAsync(string name);
    }
}