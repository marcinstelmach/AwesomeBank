namespace AwesomeBank.Api.Extensions
{
    using System;
    using System.Text;
    using AwesomeBank.Api.Mappings;
    using AwesomeBank.Api.Permissions;
    using AwesomeBank.BuildingBlocks.Infrastructure.Settings;
    using AwesomeBank.Identity.Application;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        private const string PermissionClaimType = "permission";
        private const string DatabaseSettingsSectionKey = "DatabaseSettings";
        private const string JwtAuthenticationSettingsSectionKey = "JwtAuthenticationSettings";

        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(IdentityApplicationAssembly.Assembly);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = AutoMapperConfiguration.Configure();
            services.AddTransient(_ => configuration.CreateMapper());
            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettingsSectionKey))
                .Configure<JwtAuthenticationSettings>(configuration.GetSection(JwtAuthenticationSettingsSectionKey));

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var settings = provider.GetRequiredService<IOptions<JwtAuthenticationSettings>>().Value;
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidAudience = settings.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            return services;
        }

        public static IServiceCollection AddPolicyBaseAuthorization(this IServiceCollection services)
        {
            var permissions = new ApplicationPermissionReadOnlyList();
            services.AddAuthorization(options =>
            {
                foreach (var permission in permissions)
                {
                    options.AddPolicy(permission, policy => policy.RequireClaim(PermissionClaimType, permission));
                }
            });

            return services;
        }
    }
}