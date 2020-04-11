namespace AwesomeBank.Identity.Tests
{
    using System;
    using AutoFixture;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using AwesomeBank.Tests.Common;

    public static class IdentityTestsHelper
    {
        private static readonly Fixture Fixture = new Fixture();

        public static Role CreateRole(string name = null)
        {
            var role = (Role)Activator.CreateInstance(typeof(Role), true);
            role.SetPropertyValue(nameof(Role.Id), Fixture.Create<int>());
            role.SetPropertyValue(nameof(Role.Name), name ?? Fixture.Create<string>());
            return role;
        }

        public static User CreateUser(string email = null)
        {
            return new User(
                Fixture.Create<string>(),
                Fixture.Create<string>(),
                email ?? Fixture.Create<string>(),
                Fixture.Create<Password>(),
                DateTime.UtcNow.AddYears(-18),
                Fixture.Create<IdentityDocument>(),
                CreateRole());
        }
    }
}