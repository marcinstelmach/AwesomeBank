namespace AwesomeBank.Identity.Domain.Exceptions
{
    using AwesomeBank.Identity.Domain.Entities;

    public class UserTooYoungException : IdentityBaseDomainException
    {
        public UserTooYoungException(UserId userId, int requiredAge)
            : base($"User has to be at least {requiredAge} years old.")
        {
            UserId = userId;
            RequiredAge = requiredAge;
        }

        public UserId UserId { get; }

        public int RequiredAge { get; }

        public override string Code => "user_too_young";
    }
}