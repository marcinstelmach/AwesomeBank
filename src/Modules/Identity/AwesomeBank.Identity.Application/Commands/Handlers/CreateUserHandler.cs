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
    using AwesomeBank.Identity.Domain.Specifications;
    using AwesomeBank.Identity.Domain.ValueObjects;
    using MediatR;

    internal class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IPasswordFactory _passwordFactory;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUsersRepository usersRepository, IGroupsRepository groupsRepository, IPasswordFactory passwordFactory, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _groupsRepository = groupsRepository;
            _passwordFactory = passwordFactory;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Insist.IsNotNull(request, nameof(request));

            if (await _usersRepository.ExistsUserAsync(new UserForEmailAddressSpecification(request.Email)))
            {
                throw new UserWithGivenEmailAlreadyExistsException(request.Email);
            }

            var password = _passwordFactory.Create(request.Password);
            var identityDocumentType = _mapper.Map<IdentityDocumentTypeDto, IdentityDocumentType>(request.DocumentType);
            var identityDocument = new IdentityDocument(identityDocumentType, request.DocumentValue);
            var group = await _groupsRepository.FindAsync(Consts.ClientGroupName);
            var user = new User(request.FirstName, request.LastName, request.Email, password, request.BirthdayDate, identityDocument);
            user.AssignToGroup(group);

            _usersRepository.AddUser(user);
            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}