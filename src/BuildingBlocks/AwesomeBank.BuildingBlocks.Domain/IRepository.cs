namespace AwesomeBank.BuildingBlocks.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}