namespace AwesomeBank.Identity.Domain.Entities
{
    using System.Collections.Generic;

    public class ApplicationGroup
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public virtual ICollection<ApplicationGroupClaim> ApplicationGroupClaims { get; private set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; private set; }
    }
}