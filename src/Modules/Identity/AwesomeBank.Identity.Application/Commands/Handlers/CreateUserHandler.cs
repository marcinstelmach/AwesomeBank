namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Enums;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using MediatR;

    internal class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            Insist.IsNotNull(request, nameof(request));

            var password = new Password(request.Password, string.Empty);
            var identityDocumentType = _mapper.Map<IdentityDocumentTypeDto, IdentityDocumentType>(request.DocumentType);
            var identityDocument = new IdentityDocument(identityDocumentType, request.DocumentValue);
            var user = new User(request.FirstName, request.LastName, request.Email, password, request.BirthdayDate, identityDocument);

            _usersRepository.AddUser(user);
            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}