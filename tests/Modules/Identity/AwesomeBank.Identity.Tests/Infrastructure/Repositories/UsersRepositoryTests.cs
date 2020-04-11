namespace AwesomeBank.Identity.Tests.Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.Identity.Infrastructure;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersRepositoryTests
    {
        private readonly IdentityContext _context;
        private readonly UsersRepository _sut;

        public UsersRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new IdentityContext(options);
            _sut = new UsersRepository(_context);
        }

        [Theory]
        [InlineData("test@email.com")]
        [InlineData("TEST@email.com")]
        [InlineData("TEST@Email.com")]
        public async Task When_Checking_If_User_Exists_For_Existing_Email_Address_Then_Returns_True(string email)
        {
            // Arrange
            // Lowercase is done on ef config layer, but this feature does not work on InMemory provider
            var user = IdentityTestsHelper.CreateUser(email.ToLowerInvariant());
            _sut.AddUser(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.ExistsUserAsync(email);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public async Task When_Checking_If_User_Exists_For_Non_Existing_Email_Address_Then_Returns_False(string email)
        {
            // Act
            var result = await _sut.ExistsUserAsync(email);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task When_Adding_User_Then_Adds_User_To_Database()
        {
            // Arrange
            var user = IdentityTestsHelper.CreateUser();

            // Act
            _sut.AddUser(user);
            await _context.SaveChangesAsync();

            // Assert
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            existingUser.Should().NotBeNull();
            existingUser.Should().BeEquivalentTo(user);
        }
    }
}