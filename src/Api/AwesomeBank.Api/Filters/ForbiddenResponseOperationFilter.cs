namespace AwesomeBank.Api.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ForbiddenResponseOperationFilter : IOperationFilter
    {
        private const string ForbiddenResponseCode = "403";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authorizeAttributeData =
                context.MethodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(AuthorizeAttribute));
            if (authorizeAttributeData != null)
            {
                operation.Responses.Add(
                    ForbiddenResponseCode,
                    new OpenApiResponse { Description = $"Forbidden - required permission {authorizeAttributeData.ConstructorArguments.First()}" });
            }
        }
    }
}