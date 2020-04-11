namespace AwesomeBank.Identity.Domain.Services
{
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.ValueObjects;

    public class PasswordFactory : IPasswordFactory
    {
        private readonly IPasswordEncrypter _passwordEncrypter;

        public PasswordFactory(IPasswordEncrypter passwordEncrypter)
        {
            _passwordEncrypter = passwordEncrypter;
        }

        public Password Create(string passwordText)
        {
            Insist.IsNotNullOrWhiteSpace(passwordText, nameof(passwordText));

            var salt = _passwordEncrypter.GetPasswordSalt();
            var passwordHash = _passwordEncrypter.GetPasswordHash(passwordText, salt);
            return new Password(passwordHash, salt);
        }
    }
}