namespace AwesomeBank.Identity.Tests.Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.Identity.Infrastructure;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RolesRepositoryTests
    {
        private readonly IdentityContext _context;
        private readonly RolesRepository _sut;

        public RolesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new IdentityContext(options);
            _sut = new RolesRepository(_context);
        }

        [Theory]
        [AutoData]
        public async Task When_Getting_Role_That_Exists_Then_Returns_Role(string name)
        {
            // Arrange
            var role = IdentityTestsHelper.CreateRole(name);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetRoleAsync(name);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(role);
        }

        [Theory]
        [AutoData]
        public async Task When_Getting_Role_That_Does_Not_Exist_Then_Returns_Null(string name)
        {
            // Act
            var result = await _sut.GetRoleAsync(name);

            // Assert
            result.Should().BeNull();
        }
    }
}