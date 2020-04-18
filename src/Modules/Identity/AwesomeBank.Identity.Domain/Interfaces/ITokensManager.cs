namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Models;

    public interface ITokensManager
    {
        Task<JwtToken> CreateTokenAsync(User user);
    }
}