namespace AwesomeBank.Identity.Tests.Application
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain.Specifications;
    using AwesomeBank.Identity.Application.Commands;
    using AwesomeBank.Identity.Application.Commands.Handlers;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Application.Exceptions;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.Models;
    using AwesomeBank.Identity.Domain.Specifications;
    using AwesomeBank.Tests.Common;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class SignInUserHandlerTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IPasswordComparer> _passwordComparerMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SignInUserHandler _sut;

        public SignInUserHandlerTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _passwordComparerMock = new Mock<IPasswordComparer>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _mapperMock = new Mock<IMapper>();
            _sut = new SignInUserHandler(
                _usersRepositoryMock.Object,
                _passwordComparerMock.Object,
                _jwtTokenGeneratorMock.Object,
                _mapperMock.Object);

            _usersRepositoryMock.Setup(x => x.FindUserAsync(It.IsAny<Specification<User>>()))
                .ReturnsAsync(IdentityTestsHelper.CreateUser());
            _passwordComparerMock
                .Setup(x => x.ArePasswordsEquals(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
        }

        [Fact]
        public async Task When_Handling_And_Command_Is_Null_Then_Throws_Argument_Null_Exception()
        {
            // Act
            Func<Task> func = () => _sut.Handle(null, default);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Then_Finds_User_In_Repository(SignInUserCommand command)
        {
            // Act
            await _sut.Handle(command, default);

            // Assert
            _usersRepositoryMock.Verify(x => x.FindUserAsync(It.IsAny<UserForEmailAddressSpecification>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_And_User_With_Given_Email_Does_Not_Exists_Then_Throws_Invalid_User_Email_Or_Password_Exception(SignInUserCommand command)
        {
            // Arrange
            _usersRepositoryMock.Setup(x => x.FindUserAsync(It.IsAny<Specification<User>>()))
                .ReturnsAsync((User)null);

            // Act
            Func<Task> func = () => _sut.Handle(command, default);

            // Assert
            await func.Should().ThrowAsync<InvalidUserEmailOrPasswordException>();
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_And_Given_Password_Is_Different_Than_User_Password_Then_Throws_Invalid_User_Email_Or_Password_Exception(SignInUserCommand command)
        {
            // Arrange
            _passwordComparerMock
                .Setup(x => x.ArePasswordsEquals(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            // Act
            Func<Task> func = () => _sut.Handle(command, default);

            // Assert
            await func.Should().ThrowAsync<InvalidUserEmailOrPasswordException>();
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Then_Creates_Token_In_Jwt_Token_Generator(SignInUserCommand command)
        {
            // Arrange
            var user = IdentityTestsHelper.CreateUser();
            _usersRepositoryMock.Setup(x => x.FindUserAsync(It.IsAny<Specification<User>>())).ReturnsAsync(user);

            // Act
            await _sut.Handle(command, default);

            // Assert
            _jwtTokenGeneratorMock.Verify(x => x.GenerateAsync(user), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Then_Maps_Jwt_Token_To_Token_Dto(SignInUserCommand command, JwtToken jwtToken)
        {
            // Arrange
            _jwtTokenGeneratorMock.Setup(x => x.GenerateAsync(It.IsAny<User>())).ReturnsAsync(jwtToken);

            // Act
            await _sut.Handle(command, default);

            // Assert
            _mapperMock.Verify(x => x.Map<JwtToken, TokenDto>(jwtToken), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Then_Returns_Token_Dto(SignInUserCommand command, TokenDto tokenDto)
        {
            // Arrange
            _mapperMock.Setup(x => x.Map<JwtToken, TokenDto>(It.IsAny<JwtToken>())).Returns(tokenDto);

            // Act
            var result = await _sut.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(tokenDto);
        }
    }
}