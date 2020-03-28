namespace AwesomeBank.Api.Extensions
{
    using AutoMapper;
    using AwesomeBank.Api.Mappings.Identity;
    using AwesomeBank.Identity.Application;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebApiServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(IdentityApplicationAssembly.Assembly);

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserViewModelProfile());
            });

            services.AddTransient<IMapper>(_ => configuration.CreateMapper());
            return services;
        }
    }
}