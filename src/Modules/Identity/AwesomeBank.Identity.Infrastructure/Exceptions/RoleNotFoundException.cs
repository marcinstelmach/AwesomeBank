namespace AwesomeBank.Identity.Infrastructure.Exceptions
{
    public class RoleNotFoundException : IdentityBaseInfrastructureException
    {
        public RoleNotFoundException(string roleName)
            : base($"User role with name: '{roleName}` not found.")
        {
        }

        public override string Code => "user_role_not_found";
    }
}