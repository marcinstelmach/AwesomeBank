namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Application.Exceptions;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.Models;
    using AwesomeBank.Identity.Domain.Specifications;

    internal class SignInUserHandler : ICommandHandler<SignInUserCommand, TokenDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordComparer _passwordComparer;
        private readonly ITokensManager _tokensManager;
        private readonly IMapper _mapper;

        public SignInUserHandler(IUsersRepository usersRepository, IPasswordComparer passwordComparer, ITokensManager tokensManager, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _passwordComparer = passwordComparer;
            _tokensManager = tokensManager;
            _mapper = mapper;
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

            var jwtToken = await _tokensManager.CreateTokenAsync(user);
            return _mapper.Map<JwtToken, TokenDto>(jwtToken);
        }
    }
}