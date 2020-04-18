namespace AwesomeBank.Api.Tests.Modules.Identity
{
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.Api.Modules.Identity;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Commands;
    using AwesomeBank.Identity.Application.Dtos;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AuthenticationControllerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBus> _busMock;
        private readonly AuthenticationController _sut;

        public AuthenticationControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _busMock = new Mock<IBus>();
            _sut = new AuthenticationController(_mapperMock.Object, _busMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Signing_In_Then_Maps_View_Model_To_Sign_In_Command(SignInUserViewModel viewModel)
        {
            // Act
            await _sut.SignInUserAsync(viewModel);

            // Assert
            _mapperMock.Verify(x => x.Map<SignInUserViewModel, SignInUserCommand>(viewModel), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Signing_In_Then_Executes_Command_On_Bus(SignInUserViewModel viewModel, SignInUserCommand command)
        {
            // Arrange
            _mapperMock.Setup(x => x.Map<SignInUserViewModel, SignInUserCommand>(It.IsAny<SignInUserViewModel>()))
                .Returns(command);

            // Act
            await _sut.SignInUserAsync(viewModel);

            // Assert
            _busMock.Verify(x => x.ExecuteCommandAsync(command), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Signing_In_Then_Maps_Token_Dto_To_Token_View_Model(SignInUserViewModel viewModel, TokenDto tokenDto)
        {
            // Arrange
            _busMock.Setup(x => x.ExecuteCommandAsync(It.IsAny<ICommand<TokenDto>>())).ReturnsAsync(tokenDto);

            // Act
            await _sut.SignInUserAsync(viewModel);

            // Assert
            _mapperMock.Verify(x => x.Map<TokenDto, TokenViewModel>(tokenDto), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Signing_In_Then_Returns_Ok_Object_Result(SignInUserViewModel viewModel, TokenViewModel tokenViewModel)
        {
            // Arrange
            _mapperMock.Setup(x => x.Map<TokenDto, TokenViewModel>(It.IsAny<TokenDto>())).Returns(tokenViewModel);

            // Act
            var result = await _sut.SignInUserAsync(viewModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.As<OkObjectResult>();
            objectResult.Value.Should().BeOfType<TokenViewModel>();
            objectResult.Value.Should().BeEquivalentTo(tokenViewModel);
        }
    }
}