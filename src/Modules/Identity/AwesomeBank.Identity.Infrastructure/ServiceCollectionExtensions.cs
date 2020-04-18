namespace AwesomeBank.Identity.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Infrastructure.Settings;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.Services;
    using AwesomeBank.Identity.Infrastructure.Repositories;
    using AwesomeBank.Identity.Infrastructure.Services;
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
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUserClaimsRepository, UserClaimsRepository>();
            services.AddTransient<IPasswordEncrypter, PasswordEncrypter>();
            services.AddTransient<IPasswordFactory, PasswordFactory>();
            services.AddTransient<IPasswordComparer, PasswordComparer>();
            services.AddTransient<ITokensManager, TokensManager>();

            return services;
        }
    }
}