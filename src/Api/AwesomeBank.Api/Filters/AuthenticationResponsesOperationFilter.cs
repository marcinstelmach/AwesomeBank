namespace AwesomeBank.Api.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class AuthenticationResponsesOperationFilter : IOperationFilter
    {
        private const string UnauthorizedResponseCode = "401";
        private const string UnauthorizedResponseDescription = "Unauthorized";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.CustomAttributes.All(x => x.AttributeType != typeof(AllowAnonymousAttribute)))
            {
                operation.Responses.Add(
                    UnauthorizedResponseCode,
                    new OpenApiResponse { Description = UnauthorizedResponseDescription });
            }
        }
    }
}