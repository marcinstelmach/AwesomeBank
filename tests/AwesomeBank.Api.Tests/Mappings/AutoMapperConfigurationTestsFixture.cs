namespace AwesomeBank.Api.Tests.Mappings
{
    using AutoMapper;
    using AwesomeBank.Api.Mappings;
    using Xunit;

    public class AutoMapperConfigurationTestsFixture
    {
        public AutoMapperConfigurationTestsFixture()
        {
            var configuration = AutoMapperConfiguration.Configure();
            Mapper = configuration.CreateMapper();
        }

        public IMapper Mapper { get; }

        [Fact]
        public void When_Configuring_AutoMapper_Then_Configuration_Is_Valid()
        {
            // Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}