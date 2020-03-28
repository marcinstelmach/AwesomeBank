namespace AwesomeBank.BuildingBlocks.Application
{
    using MediatR;

    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}