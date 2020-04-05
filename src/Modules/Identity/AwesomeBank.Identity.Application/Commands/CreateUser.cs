namespace AwesomeBank.Identity.Application.Commands
{
    using System;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Dtos;

    public class CreateUser : ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthdayDate { get; set; }

        public IdentityDocumentTypeDto DocumentType { get; set; }

        public string DocumentValue { get; set; }
    }
}