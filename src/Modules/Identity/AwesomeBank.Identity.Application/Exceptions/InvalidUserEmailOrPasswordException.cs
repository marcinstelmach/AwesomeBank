namespace AwesomeBank.Identity.Application.Exceptions
{
    using System.Net;
    using AwesomeBank.BuildingBlocks.Domain;

    public class InvalidUserEmailOrPasswordException : ApplicationBaseException
    {
        public InvalidUserEmailOrPasswordException()
            : base("Invalid user email or password.")
        {
        }

        public override string Code => "invalid_user_email_or_password";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}