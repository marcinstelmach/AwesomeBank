namespace AwesomeBank.Api.Permissions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AwesomeBank.Api.Filters;

    public class ApplicationPermissionReadOnlyList : IReadOnlyCollection<string>
    {
        private readonly List<string> _permissions;

        public ApplicationPermissionReadOnlyList()
        {
            _permissions = GetPermissions();
        }

        public int Count => _permissions.Count;

        public IEnumerator<string> GetEnumerator()
            => _permissions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static List<string> GetPermissions()
        {
            return Assembly.GetAssembly(typeof(PermissionSetAttribute))
                .GetTypes()
                .Where(x => x.GetCustomAttribute<PermissionSetAttribute>() != null)
                .SelectMany(x => x.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(x => x.GetValue(null))
                    .Cast<string>())
                .ToList();
        }
    }
}