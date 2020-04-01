namespace AwesomeBank.Identity.Domain.Entities
{
    using System.Collections.Generic;
    using AwesomeBank.BuildingBlocks.Domain;

    public class Password : ValueObject<Password>
    {
        public Password(string passwordHash, string securityStamp)
        {
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
        }

        protected Password()
        {
        }

        public string PasswordHash { get; private set; }

        public string SecurityStamp { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PasswordHash;
            yield return SecurityStamp;
        }
    }
}