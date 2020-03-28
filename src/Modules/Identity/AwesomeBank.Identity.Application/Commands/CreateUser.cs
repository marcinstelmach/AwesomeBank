namespace AwesomeBank.Identity.Application.Commands
{
    using AwesomeBank.BuildingBlocks.Application;

    public class CreateUser : ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}