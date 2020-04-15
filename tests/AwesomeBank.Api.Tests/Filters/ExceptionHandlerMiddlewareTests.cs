namespace AwesomeBank.Api.Tests.Filters
{
    using System.Threading.Tasks;
    using AwesomeBank.Api.Filters;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class ExceptionHandlerMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextDelegateMock;
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly ExceptionHandlerMiddleware _sut;

        public ExceptionHandlerMiddlewareTests()
        {
            _nextDelegateMock = new Mock<RequestDelegate>();
            _httpContextMock = new Mock<HttpContext>();
            _sut = new ExceptionHandlerMiddleware(_nextDelegateMock.Object);
        }

        [Fact]
        public async Task When_Invoked_Then_Invoke_Next_Delegate_With_Given_Context()
        {
            // Act
            await _sut.Invoke(_httpContextMock.Object);

            // Assert
            _nextDelegateMock.Verify(x => x.Invoke(_httpContextMock.Object), Times.Once);
        }
    }
}