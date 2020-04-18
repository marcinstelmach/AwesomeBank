namespace AwesomeBank.Identity.Domain.Interfaces
{
    public interface IPasswordComparer
    {
        bool ArePasswordsEquals(string givenPassword, string passwordHash, string salt);
    }
}