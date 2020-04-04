namespace AwesomeBank.Identity.Domain.Entities
{
    public class ApplicationUserGroup
    {
        public UserId UserId { get; private set; }

        public int ApplicationGroupId { get; private set; }

        public virtual ApplicationGroup ApplicationGroup { get; private set; }
    }
}