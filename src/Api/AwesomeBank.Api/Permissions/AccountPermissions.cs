namespace AwesomeBank.Api.Permissions
{
    using AwesomeBank.Api.Filters;

    [PermissionSet]
    public static class AccountPermissions
    {
        public const string Get = "account.get";
        public const string Manage = "account.manage";
    }
}