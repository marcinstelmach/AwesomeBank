namespace AwesomeBank.Identity.Domain.Models
{
    public class JwtToken
    {
        public string AccessToken { get; set; }

        public int LifetimeInMinutes { get; set; }
    }
}