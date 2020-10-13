namespace AwesomeBank.Identity.Domain.Entities
{
    using System.Collections.Generic;

    public class Group
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string NormalizedName { get; private set; }

        public virtual ICollection<GroupClaim> GroupClaims { get; private set; }
    }
}