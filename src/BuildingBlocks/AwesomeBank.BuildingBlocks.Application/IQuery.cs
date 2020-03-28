namespace AwesomeBank.BuildingBlocks.Application
{
    using MediatR;

    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}