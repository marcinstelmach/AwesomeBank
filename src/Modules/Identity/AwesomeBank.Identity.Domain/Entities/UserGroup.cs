namespace AwesomeBank.Identity.Domain.Entities
{
    public class UserGroup
    {
        public UserGroup(UserId userId, int groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }

        private UserGroup()
        {
        }

        public UserId UserId { get; private set; }

        public int GroupId { get; private set; }

        public virtual Group Group { get; private set; }
    }
}