namespace AwesomeBank.Identity.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using MediatR;

    internal class CreateUserHandler : ICommandHandler<CreateUser>
    {
        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}