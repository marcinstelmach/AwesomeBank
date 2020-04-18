namespace AwesomeBank.Api.Extensions
{
    using AutoMapper;
    using AwesomeBank.Api.Mappings;
    using AwesomeBank.BuildingBlocks.Infrastructure.Settings;
    using AwesomeBank.Identity.Application;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        private const string DatabaseSettingsSectionKey = "DatabaseSettings";
        private const string JwtAuthenticationSettingsSectionKey = "JwtAuthenticationSettings";

        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(IdentityApplicationAssembly.Assembly);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = AutoMapperConfiguration.Configure();
            services.AddTransient<IMapper>(_ => configuration.CreateMapper());
            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettingsSectionKey))
                .Configure<JwtAuthenticationSettings>(configuration.GetSection(JwtAuthenticationSettingsSectionKey));
    }
}