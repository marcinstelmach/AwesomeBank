namespace AwesomeBank.Identity.Tests.Domain.Services
{
    using System;
    using AutoFixture.Xunit2;
    using AwesomeBank.Identity.Domain.Factories;
    using AwesomeBank.Identity.Domain.Interfaces;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class PasswordFactoryTests
    {
        private readonly Mock<IPasswordEncrypter> _passwordEncrypterMock;
        private readonly PasswordFactory _sut;

        public PasswordFactoryTests()
        {
            _passwordEncrypterMock = new Mock<IPasswordEncrypter>();
            _sut = new PasswordFactory(_passwordEncrypterMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void When_Creating_Password_And_Given_Password_Text_Is_Null_Or_White_Space_Then_Throws_Argument_Exception(string passwordText)
        {
            // Act
            Action action = () => _sut.Create(passwordText);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [AutoData]
        public void When_Creating_Password_Then_Gets_Password_Salt_From_Encrypter(string passwordText, string passwordHash, string salt)
        {
            // Arrange
            _passwordEncrypterMock.Setup(x => x.GetPasswordHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(passwordHash);
            _passwordEncrypterMock.Setup(x => x.GetPasswordSalt()).Returns(salt);

            // Act
            _sut.Create(passwordText);

            // Assert
            _passwordEncrypterMock.Verify(x => x.GetPasswordSalt(), Times.Once);
        }

        [Theory]
        [AutoData]
        public void When_Creating_Password_Then_Gets_Password_Hash_From_Encrypter(string passwordText, string passwordHash, string salt)
        {
            // Arrange
            _passwordEncrypterMock.Setup(x => x.GetPasswordHash(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(passwordHash);
            _passwordEncrypterMock.Setup(x => x.GetPasswordSalt()).Returns(salt);

            // Act
            _sut.Create(passwordText);

            // Assert
            _passwordEncrypterMock.Verify(x => x.GetPasswordHash(passwordText, salt), Times.Once);
        }
    }
}