namespace AwesomeBank.Identity.Domain.Entities
{
    public class ApplicationGroupRole
    {
        public int ApplicationGroupId { get; private set; }

        public int RoleId { get; private set; }

        public virtual Role Role { get; private set; }
    }
}