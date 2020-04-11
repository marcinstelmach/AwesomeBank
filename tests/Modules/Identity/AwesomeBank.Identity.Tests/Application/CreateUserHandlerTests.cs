namespace AwesomeBank.Identity.Tests.Application
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture;
    using AutoFixture.Xunit2;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Commands;
    using AwesomeBank.Identity.Application.Commands.Handlers;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Application.Exceptions;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Enums;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using AwesomeBank.Tests.Common;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;

    public class CreateUserHandlerTests
    {
        private const string ClientRoleName = "Client";

        private readonly Fixture _fixture;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IRolesRepository> _rolesRepositoryMock;
        private readonly Mock<IPasswordFactory> _passwordFactoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateUserHandler _sut;

        public CreateUserHandlerTests()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _usersRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _rolesRepositoryMock = new Mock<IRolesRepository>();
            _passwordFactoryMock = new Mock<IPasswordFactory>();
            _mapperMock = new Mock<IMapper>();
            _sut = new CreateUserHandler(_usersRepositoryMock.Object, _rolesRepositoryMock.Object, _passwordFactoryMock.Object, _mapperMock.Object);

            _usersRepositoryMock.Setup(x => x.ExistsUserAsync(It.IsAny<string>())).ReturnsAsync(false);
        }

        [Fact]
        public async Task When_Handling_Command_And_Create_User_Command_is_Null_Then_Throws_Argument_Null_Exception()
        {
            // Act
            Func<Task> func = () => _sut.Handle(null, default);

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task When_Handling_Command_Then_Checks_If_User_With_Given_Email_Exists_in_Repository()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            await _sut.Handle(request, default);

            // Assert
            _usersRepositoryMock.Verify(x => x.ExistsUserAsync(request.Email), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Command_And_User_With_Given_Email_Already_Exists_Then_Throws_Exception(CreateUser request)
        {
            // Arrange
            _usersRepositoryMock.Setup(x => x.ExistsUserAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            Func<Task> func = () => _sut.Handle(request, default);

            // Assert
            await func.Should().ThrowAsync<UserWithGivenEmailAlreadyExistsException>();
        }

        [Fact]
        public async Task When_Handling_Command_Then_Gets_Client_Role_From_Repository()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            await _sut.Handle(request, default);

            // Assert
            _rolesRepositoryMock.Verify(x => x.GetRoleAsync(ClientRoleName), Times.Once);
        }

        [Fact]
        public async Task When_Handling_Command_Then_Creates_Password_In_Factory()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            await _sut.Handle(request, default);

            // Assert
            _passwordFactoryMock.Verify(x => x.Create(request.Password), Times.Once);
        }

        [Fact]
        public async Task When_Handling_Command_Then_Maps_Identity_Document_Type_Dto_To_Identity_Document_Type()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            await _sut.Handle(request, default);

            // Assert
            _mapperMock.Verify(x => x.Map<IdentityDocumentTypeDto, IdentityDocumentType>(request.DocumentType), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Handling_Command_Then_Adds_User_In_Repository(Password password, IdentityDocumentType documentType)
        {
            // Arrange
            var request = CreateRequest();
            var role = CreateRole();
            _passwordFactoryMock.Setup(x => x.Create(It.IsAny<string>())).Returns(password);
            _mapperMock.Setup(x => x.Map<IdentityDocumentTypeDto, IdentityDocumentType>(It.IsAny<IdentityDocumentTypeDto>()))
                .Returns(documentType);
            _rolesRepositoryMock.Setup(x => x.GetRoleAsync(It.IsAny<string>())).ReturnsAsync(role);

            // Act
            await _sut.Handle(request, default);

            // Assert
            _usersRepositoryMock.Verify(
                x => x.AddUser(
                    It.Is<User>(y =>
                        y.FirstName == request.FirstName &&
                        y.LastName == request.LastName &&
                        y.Email == request.Email &&
                        y.Password == password &&
                        y.BirthDayDate == request.BirthdayDate &&
                        y.IdentityDocument.Type == documentType &&
                        y.IdentityDocument.Value == request.DocumentValue &&
                        y.Role == role)),
                Times.Once);
        }

        [Fact]
        public async Task When_Handling_Command_Then_Saves_Changes_In_Unit_Of_Work()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            await _sut.Handle(request, default);

            // Assert
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task When_Handling_Command_Then_Returns_Value_Of_Unit()
        {
            // Arrange
            var request = CreateRequest();

            // Act
            var result = await _sut.Handle(request, default);

            // Assert
            result.Should().Be(Unit.Value);
        }

        private CreateUser CreateRequest()
        {
            return _fixture
                .Build<CreateUser>()
                .With(x => x.BirthdayDate, DateTime.UtcNow.Date.AddYears(-18))
                .Create();
        }

        private Role CreateRole()
        {
            var role = (Role)Activator.CreateInstance(typeof(Role), true);
            role.SetPropertyValue(nameof(Role.Id), _fixture.Create<int>());
            role.SetPropertyValue(nameof(Role.Name), _fixture.Create<string>());
            return role;
        }
    }
}