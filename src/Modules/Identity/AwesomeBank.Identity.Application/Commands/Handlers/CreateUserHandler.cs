namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Enums;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using MediatR;

    internal class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private const string ClientRoleName = "Client";

        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordFactory _passwordFactory;
        private readonly IRolesRepository _rolesRepository;

        public CreateUserHandler(IUsersRepository usersRepository, IMapper mapper, IPasswordFactory passwordFactory, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _passwordFactory = passwordFactory;
            _rolesRepository = rolesRepository;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            Insist.IsNotNull(request, nameof(request));

            var password = _passwordFactory.Create(request.Password);
            var identityDocumentType = _mapper.Map<IdentityDocumentTypeDto, IdentityDocumentType>(request.DocumentType);
            var identityDocument = new IdentityDocument(identityDocumentType, request.DocumentValue);
            var role = await _rolesRepository.GetRoleAndEnsureExistsAsync(ClientRoleName);
            var user = new User(request.FirstName, request.LastName, request.Email, password, request.BirthdayDate, identityDocument, role);

            _usersRepository.AddUser(user);
            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}