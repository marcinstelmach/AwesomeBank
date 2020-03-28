namespace AwesomeBank.BuildingBlocks.Infrastructure.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AwesomeBank.BuildingBlocks.Application;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;

    public class MediatrBusTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly MediatrBus _sut;

        public MediatrBusTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new MediatrBus(_mediatorMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Executing_Command_Then_Sends_Command_Through_Mediator(FakeCommand command)
        {
            // Act
            await _sut.ExecuteCommandAsync(command);

            // Assert
            _mediatorMock.Verify(x => x.Send(command, default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Executing_Returning_Command_Then_Sends_Command_Through_Mediator(FakeReturningCommand command)
        {
            // Act
            await _sut.ExecuteCommandAsync(command);

            // Assert
            _mediatorMock.Verify(x => x.Send(command, default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Executing_Returning_Command_Then_Returns_Response_From_Mediator(FakeReturningCommand command, string response)
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<ICommand<string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _sut.ExecuteCommandAsync(command);

            // Assert
            result.Should().Be(response);
        }

        [Theory]
        [AutoData]
        public async Task When_Executing_Query_Then_Sends_Query_Through_Mediator(FakeQuery query)
        {
            // Act
            await _sut.ExecuteQueryAsync(query);

            // Assert
            _mediatorMock.Verify(x => x.Send(query, default), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Executing_Query_Then_Returns_Response_From_Mediator(FakeQuery query, string response)
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<IQuery<string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _sut.ExecuteQueryAsync(query);

            // Assert
            result.Should().Be(response);
        }
    }
}