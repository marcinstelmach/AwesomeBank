namespace AwesomeBank.Identity.Domain.Interfaces
{
    public interface IPasswordEncrypter
    {
        string GetPasswordSalt();

        string GetPasswordHash(string password, string salt);
    }
}