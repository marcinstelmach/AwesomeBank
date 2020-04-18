namespace AwesomeBank.BuildingBlocks.Infrastructure.Extensions
{
    using AwesomeBank.BuildingBlocks.Application;
    using Microsoft.Extensions.DependencyInjection;

    public static class BuildingBlocksServiceCollectionExtensions
    {
        public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
            => services
                .AddScoped<IBus, MediatrBus>()
                .AddScoped<IMapper, Mapper>()
                .AddTransient<IDateTimeService, DateTimeService>();
    }
}