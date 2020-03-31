namespace AwesomeBank.BuildingBlocks.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Domain;

    public class Repository : IRepository
    {
        public Repository(IUnitOfWork context)
        {
            Insist.IsNotNull(context, nameof(context));
            UnitOfWork = context;
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}