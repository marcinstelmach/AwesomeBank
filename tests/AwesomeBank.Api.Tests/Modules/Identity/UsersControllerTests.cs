namespace AwesomeBank.Api.Tests.Modules.Identity
{
    using System.Threading.Tasks;
    using AwesomeBank.Api.Modules.Identity;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class UsersControllerTests
    {
        private readonly UsersController _sut;

        public UsersControllerTests()
        {
            _sut = new UsersController();
        }

        [Fact]
        public async Task When_Testing_Then_Returns_Ok_Object_Result_With_Hello_World_Message()
        {
            // Act
            var result = await _sut.TestAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.As<OkObjectResult>();
            objectResult.Value.ToString().Should().Be("Hello world");
        }
    }
}