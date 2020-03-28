namespace AwesomeBank.Api.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture.Xunit2;
    using AwesomeBank.Api.Filters;
    using AwesomeBank.Api.Models;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Moq;
    using Xunit;

    public class ValidateModelFilterAttributeTests
    {
        private readonly ActionExecutingContext _actionExecutedContext;
        private readonly ModelStateDictionary _modelStateDictionary;
        private readonly ValidateModelFilterAttribute _sut;

        public ValidateModelFilterAttributeTests()
        {
            _modelStateDictionary = new ModelStateDictionary();
            var actionContext = new ActionContext(
                new Mock<HttpContext>().Object,
                new Mock<RouteData>().Object,
                new Mock<ActionDescriptor>().Object,
                _modelStateDictionary);
            _actionExecutedContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);
            _sut = new ValidateModelFilterAttribute();
        }

        [Fact]
        public void When_Executing_And_Model_State_Is_Valid_Then_Does_Not_Modify_Context_Result()
        {
            // Arrange
            var expectedResult = _actionExecutedContext.Result;

            // Act
            _sut.OnActionExecuting(_actionExecutedContext);

            // Assert
            _actionExecutedContext.Result.Should().Be(expectedResult);
        }

        [Theory]
        [AutoData]
        public void When_Executing_And_Model_State_Is_Not_Valid_Then_Returns_Bad_Request_Object_Result(string key, string errorMessage)
        {
            // Arrange
            _modelStateDictionary.AddModelError(key, errorMessage);

            // Act
            _sut.OnActionExecuting(_actionExecutedContext);

            // Assert
            _actionExecutedContext.Result.Should().NotBeNull();
            _actionExecutedContext.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [AutoData]
        public void When_Executing_And_Model_State_Is_Not_Valid_Then_Returns_Error_Response_View_Model(string key, string errorMessage)
        {
            // Arrange
            _modelStateDictionary.AddModelError(key, errorMessage);

            // Act
            _sut.OnActionExecuting(_actionExecutedContext);

            // Assert
            var objectResult = _actionExecutedContext.Result.As<BadRequestObjectResult>();
            objectResult.Value.Should().BeOfType<ErrorResponseViewModel>();
            var viewModel = objectResult.Value.As<ErrorResponseViewModel>();
            viewModel.Message.Should().NotBeEmpty();
            viewModel.Code.Should().NotBeEmpty();
            viewModel.Errors.Should().HaveCount(1);
            viewModel.Errors.First().Field.Should().Be(key);
            viewModel.Errors.First().Message.Should().Be(errorMessage);
        }
    }
}