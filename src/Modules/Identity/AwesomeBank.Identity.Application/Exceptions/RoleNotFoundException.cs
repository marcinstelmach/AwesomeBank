namespace AwesomeBank.Identity.Application.Exceptions
{
    using System.Net;
    using AwesomeBank.BuildingBlocks.Domain;

    public class RoleNotFoundException : ApplicationBaseException
    {
        public RoleNotFoundException(string roleName)
            : base($"User role with name: '{roleName}` not found.")
        {
            RoleName = roleName;
        }

        public string RoleName { get; }

        public override string Code => "user_role_not_found";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}