namespace AwesomeBank.Identity.Tests
{
    using System;
    using AutoFixture;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.ValueObjects;

    public static class IdentityTestsHelper
    {
        private static readonly Fixture Fixture = new Fixture();

        public static User CreateUser(string email = null)
        {
            return new User(
                Fixture.Create<string>(),
                Fixture.Create<string>(),
                email ?? Fixture.Create<string>(),
                Fixture.Create<Password>(),
                DateTime.UtcNow.AddYears(-18),
                Fixture.Create<IdentityDocument>());
        }
    }
}