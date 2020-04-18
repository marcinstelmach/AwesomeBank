namespace AwesomeBank.Api.Mappings
{
    using AutoMapper;
    using AwesomeBank.Api.Mappings.Identity;
    using AwesomeBank.Identity.Infrastructure.Mappings;

    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserViewModelProfile());
                cfg.AddProfile(new AuthenticationViewModelsProfile());
                cfg.AddProfile(new JwtTokenProfile());
            });

            return configuration;
        }
    }
}