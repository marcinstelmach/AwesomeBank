namespace AwesomeBank.Api.Extensions
{
    using Microsoft.AspNetCore.Builder;

    // ReSharper disable once InconsistentNaming
    public static class IApplicationBuilderExtensions
    {
        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Awesome Bank"); });
        }
    }
}