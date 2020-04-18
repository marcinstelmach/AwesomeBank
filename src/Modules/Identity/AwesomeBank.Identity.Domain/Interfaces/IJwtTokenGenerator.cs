namespace AwesomeBank.Identity.Domain.Interfaces
{
    using System.Threading.Tasks;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Models;

    public interface IJwtTokenGenerator
    {
        Task<JwtToken> GenerateAsync(User user);
    }
}