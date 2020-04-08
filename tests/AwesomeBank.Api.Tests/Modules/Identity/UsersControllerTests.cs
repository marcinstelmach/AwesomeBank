namespace AwesomeBank.Api.Tests.Modules.Identity
{
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.Api.Modules.Identity;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Commands;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class UsersControllerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBus> _busMock;
        private readonly UsersController _sut;

        public UsersControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _busMock = new Mock<IBus>();
            _sut = new UsersController(_mapperMock.Object, _busMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Creating_User_Then_Maps_Create_User_View_Model_To_Create_User_Command(CreateUserViewModel model)
        {
            // Act
            await _sut.CreateUserAsync(model);

            // Assert
            _mapperMock.Verify(x => x.Map<CreateUserViewModel, CreateUser>(model), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Creating_User_Then_Executes_Command_On_Bus(CreateUserViewModel model, CreateUser createUser)
        {
            // Arrange
            _mapperMock.Setup(x => x.Map<CreateUserViewModel, CreateUser>(It.IsAny<CreateUserViewModel>()))
                .Returns(createUser);

            // Act
            await _sut.CreateUserAsync(model);

            // Assert
            _busMock.Verify(x => x.ExecuteCommandAsync(createUser), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Creating_User_Then_Returns_Accepted_Result(CreateUserViewModel model)
        {
            // Act
            var result = await _sut.CreateUserAsync(model);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AcceptedResult>();
        }
    }
}