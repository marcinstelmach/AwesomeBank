namespace AwesomeBank.Identity.Application.Exceptions
{
    using System.Net;
    using AwesomeBank.BuildingBlocks.Domain;

    public class UserWithGivenEmailAlreadyExistsException : ApplicationBaseException
    {
        public UserWithGivenEmailAlreadyExistsException(string email)
            : base($"User with email: '{email}' already exists in database.")
        {
            Email = email;
        }

        public string Email { get; }

        public override string Code => "user_with_given_email_already_exists";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}