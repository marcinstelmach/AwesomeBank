namespace AwesomeBank.Api.Modules.Identity.Models
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }

        public int LifetimeInMinutes { get; set; }
    }
}