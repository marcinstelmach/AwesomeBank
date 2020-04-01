namespace AwesomeBank.Identity.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Infrastructure.Settings;
    using AwesomeBank.Identity.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var databaseSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            services.
                AddDbContext<IdentityContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(databaseSettings.AwesomeBankConnectionString));

            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}