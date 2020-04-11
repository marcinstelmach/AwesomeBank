namespace AwesomeBank.Identity.Tests.Infrastructure.Services
{
    using System;
    using AutoFixture.Xunit2;
    using AwesomeBank.Identity.Infrastructure.Services;
    using FluentAssertions;
    using Xunit;

    public class PasswordEncrypterTests
    {
        private readonly PasswordEncrypter _sut;

        public PasswordEncrypterTests()
        {
            _sut = new PasswordEncrypter();
        }

        [Fact]
        public void When_Getting_Password_Salt_Then_Returns_Correct_Not_Empty_String()
        {
            // Act
            var result = _sut.GetPasswordSalt();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        public void When_Getting_Password_Hash_And_Given_Password_Is_Null_Or_Whitespace_Then_Throws_Argument_Exception(
            string password, string salt)
        {
            // Act
            Func<string> func = () => _sut.GetPasswordHash(password, salt);

            // Act
            func.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        public void When_Getting_Password_Hash_And_Given_Salt_Is_Null_Or_Whitespace_Then_Throws_Argument_Exception(
            string salt, string password)
        {
            // Act
            Func<string> func = () => _sut.GetPasswordHash(password, salt);

            // Act
            func.Should().Throw<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void When_Getting_Password_Hash_Then_Returns_Correct_Password_Hash(string password, string salt)
        {
            // Act
            var result = _sut.GetPasswordHash(password, salt);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}