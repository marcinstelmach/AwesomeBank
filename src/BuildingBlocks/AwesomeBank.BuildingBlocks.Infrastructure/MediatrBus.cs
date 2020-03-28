namespace AwesomeBank.BuildingBlocks.Infrastructure
{
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using MediatR;

    public class MediatrBus : IBus
    {
        private readonly IMediator _mediator;

        public MediatrBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExecuteCommandAsync(ICommand command)
            => await _mediator.Send(command);

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
            => await _mediator.Send(command);

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
            => await _mediator.Send(query);
    }
}