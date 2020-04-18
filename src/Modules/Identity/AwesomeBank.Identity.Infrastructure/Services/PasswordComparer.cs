namespace AwesomeBank.Identity.Infrastructure.Services
{
    using System;
    using AwesomeBank.Identity.Domain.Interfaces;

    public class PasswordComparer : IPasswordComparer
    {
        private readonly IPasswordEncrypter _passwordEncrypter;

        public PasswordComparer(IPasswordEncrypter passwordEncrypter)
        {
            _passwordEncrypter = passwordEncrypter;
        }

        public bool ArePasswordsEquals(string givenPassword, string passwordHash, string salt)
        {
            var givenPasswordHash = _passwordEncrypter.GetPasswordHash(givenPassword, salt);
            return givenPasswordHash.Equals(passwordHash, StringComparison.InvariantCulture);
        }
    }
}