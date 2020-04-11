namespace AwesomeBank.Identity.Application.Exceptions
{
    public class RoleNotFoundException : IdentityBaseApplicationException
    {
        public RoleNotFoundException(string roleName)
            : base($"User role with name: '{roleName}` not found.")
        {
            RoleName = roleName;
        }

        public string RoleName { get; }

        public override string Code => "user_role_not_found";
    }
}