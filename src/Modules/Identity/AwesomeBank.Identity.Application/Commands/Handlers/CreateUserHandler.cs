namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Application.Exceptions;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Enums;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using MediatR;

    internal class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private const string ClientRoleName = "Client";

        private readonly IUsersRepository _usersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IPasswordFactory _passwordFactory;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUsersRepository usersRepository, IRolesRepository rolesRepository, IPasswordFactory passwordFactory, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _passwordFactory = passwordFactory;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Insist.IsNotNull(request, nameof(request));

            if (await _usersRepository.ExistsUserAsync(request.Email))
            {
                throw new UserWithGivenEmailAlreadyExistsException(request.Email);
            }

            var role = await _rolesRepository.GetRoleAsync(ClientRoleName);
            var password = _passwordFactory.Create(request.Password);
            var identityDocumentType = _mapper.Map<IdentityDocumentTypeDto, IdentityDocumentType>(request.DocumentType);
            var identityDocument = new IdentityDocument(identityDocumentType, request.DocumentValue);
            var user = new User(request.FirstName, request.LastName, request.Email, password, request.BirthdayDate, identityDocument, role);

            _usersRepository.AddUser(user);
            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}