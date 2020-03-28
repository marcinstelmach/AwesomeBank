namespace AwesomeBank.Api.Mappings
{
    using AutoMapper;
    using AwesomeBank.Api.Mappings.Identity;

    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserViewModelProfile());
            });

            return configuration;
        }
    }
}