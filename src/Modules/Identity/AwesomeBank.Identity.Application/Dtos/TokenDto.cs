namespace AwesomeBank.Identity.Application.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public int LifetimeInMinutes { get; set; }
    }
}