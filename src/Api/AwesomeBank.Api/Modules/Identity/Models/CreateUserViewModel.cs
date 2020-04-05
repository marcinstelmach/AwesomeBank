namespace AwesomeBank.Api.Modules.Identity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(70)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(70)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime? BirthdayDate { get; set; }

        [Required]
        [EnumDataType(typeof(IdentityDocumentTypeViewModel))]
        public IdentityDocumentTypeViewModel DocumentType { get; set; }

        [Required]
        [MaxLength(256)]
        public string DocumentValue { get; set; }
    }
}