namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Application.Exceptions;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.Specifications;

    internal class SignInUserHandler : ICommandHandler<SignInUserCommand, TokenDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordComparer _passwordComparer;

        public SignInUserHandler(IUsersRepository usersRepository, IPasswordComparer passwordComparer)
        {
            _usersRepository = usersRepository;
            _passwordComparer = passwordComparer;
        }

        public async Task<TokenDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            Insist.IsNotNull(request, nameof(request));

            var specification = new UserForEmailAddressSpecification(request.Email);
            var user = await _usersRepository.FindUserAsync(specification);

            if (user is null ||
                !_passwordComparer.ArePasswordsEquals(request.Password, user.Password.PasswordHash, user.Password.SecurityStamp))
            {
                throw new InvalidUserEmailOrPasswordException();
            }

            throw new System.NotImplementedException();
        }
    }
}