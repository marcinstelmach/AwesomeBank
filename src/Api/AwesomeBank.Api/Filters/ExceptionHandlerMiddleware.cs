namespace AwesomeBank.Api.Filters
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AwesomeBank.Api.Models;
    using AwesomeBank.BuildingBlocks.Domain;
    using Microsoft.AspNetCore.Http;

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextDelegate.Invoke(context);
            }
            catch (ApplicationBaseException exception)
            {
                var errorViewModel = new ErrorResponseViewModel
                {
                    Code = exception.Code,
                    Message = exception.Message,
                    Errors = Enumerable.Empty<ErrorViewModel>()
                };
                await WriteContextResponseAsync(context, errorViewModel, exception.StatusCode);
            }
            catch (Exception exception)
            {
                var errorViewModel = new ErrorResponseViewModel
                {
                    Code = exception.GetType().Name,
                    Message = GetExceptionMessage(exception),
                    Errors = Enumerable.Empty<ErrorViewModel>()
                };
                await WriteContextResponseAsync(context, errorViewModel, HttpStatusCode.InternalServerError);
            }
        }

        private async Task WriteContextResponseAsync(HttpContext context, ErrorResponseViewModel viewModel, HttpStatusCode statusCode)
        {
            var body = JsonSerializer.Serialize(viewModel);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(body);
        }

        private string GetExceptionMessage(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(exception.Message);
            while (exception.InnerException != null)
            {
                stringBuilder.Append($"\nInnerException: {exception.InnerException.Message}");
                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}