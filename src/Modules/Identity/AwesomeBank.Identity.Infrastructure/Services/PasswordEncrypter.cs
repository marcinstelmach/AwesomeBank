namespace AwesomeBank.Identity.Infrastructure.Services
{
    using System;
    using System.Security.Cryptography;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Interfaces;

    public class PasswordEncrypter : IPasswordEncrypter
    {
        private const int SaltSize = 40;
        private const int DeriveBytesIterationsCount = 10000;

        public string GetPasswordSalt()
        {
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetPasswordHash(string password, string salt)
        {
            Insist.IsNotNullOrWhiteSpace(password, nameof(password));
            Insist.IsNotNullOrWhiteSpace(salt, nameof(salt));

            var pbkdf2 = new Rfc2898DeriveBytes(password, GetBytes(salt), DeriveBytesIterationsCount);
            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}