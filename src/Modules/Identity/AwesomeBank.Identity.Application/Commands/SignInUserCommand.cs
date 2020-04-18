namespace AwesomeBank.Identity.Application.Commands
{
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.Identity.Application.Dtos;

    public class SignInUserCommand : ICommand<TokenDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}