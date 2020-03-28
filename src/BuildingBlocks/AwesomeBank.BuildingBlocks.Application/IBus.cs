namespace AwesomeBank.BuildingBlocks.Application
{
    using System.Threading.Tasks;

    public interface IBus
    {
        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}