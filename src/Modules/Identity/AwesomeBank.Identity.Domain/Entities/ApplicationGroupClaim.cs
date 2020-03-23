namespace AwesomeBank.Identity.Domain.Entities
{
    public class ApplicationGroupClaim
    {
        public int ApplicationGroupId { get; private set; }

        public int ClaimId { get; private set; }

        public virtual Claim Claim { get; private set; }
    }
}