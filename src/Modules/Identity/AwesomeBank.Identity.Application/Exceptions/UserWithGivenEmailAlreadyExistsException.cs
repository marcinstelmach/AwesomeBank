namespace AwesomeBank.Identity.Application.Exceptions
{
    public class UserWithGivenEmailAlreadyExistsException : IdentityBaseApplicationException
    {
        public UserWithGivenEmailAlreadyExistsException(string email)
            : base($"User with email: '{email}' already exists in database.")
        {
            Email = email;
        }

        public string Email { get; }

        public override string Code => "user_with_given_email_already_exists";
    }
}