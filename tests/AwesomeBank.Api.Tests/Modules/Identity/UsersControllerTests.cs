namespace AwesomeBank.Api.Tests.Modules.Identity
{
    using System.IO.Compression;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.Api.Modules.Identity;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.BuildingBlocks.Application;
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
        public async Task When_Testing_Then_Returns_Ok_Object_Result_With_Hello_World_Message(CreateUserViewModel viewModel)
        {
            // Act
            var result = await _sut.CreateUserAsync(viewModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.As<OkObjectResult>();
            objectResult.Value.ToString().Should().Be("Hello world");
        }
    }
}