namespace AwesomeBank.Api.Modules.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SignInUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}