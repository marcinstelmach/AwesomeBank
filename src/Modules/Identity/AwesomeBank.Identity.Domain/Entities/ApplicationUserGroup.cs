namespace AwesomeBank.Identity.Domain.Entities
{
    using System.Collections.Generic;

    public class ApplicationUserGroup
    {
        public UserId UserId { get; private set; }

        public int ApplicationGroupId { get; private set; }

        public virtual ICollection<ApplicationGroup> ApplicationGroups { get; private set; }
    }
}