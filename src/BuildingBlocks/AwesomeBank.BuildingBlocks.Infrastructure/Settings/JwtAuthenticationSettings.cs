namespace AwesomeBank.BuildingBlocks.Infrastructure.Settings
{
    public class JwtAuthenticationSettings
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int AccessTokenLifetimeMinutes { get; set; }
    }
}