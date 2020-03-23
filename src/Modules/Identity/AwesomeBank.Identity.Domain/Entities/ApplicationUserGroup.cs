namespace AwesomeBank.Identity.Domain.Entities
{
    using System.Collections.Generic;

    public class ApplicationUserGroup
    {
        public int UserId { get; private set; }

        public int ApplicationUserGroupId { get; private set; }

        public virtual ICollection<ApplicationGroup> ApplicationGroups { get; private set; }
    }
}