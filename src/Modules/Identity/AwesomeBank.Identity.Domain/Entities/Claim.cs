namespace AwesomeBank.Identity.Domain.Entities
{
    public class Claim
    {
        public int Id { get; private set; }

        public string ClaimType { get; private set; }

        public string ClaimValue { get; private set; }
    }
}