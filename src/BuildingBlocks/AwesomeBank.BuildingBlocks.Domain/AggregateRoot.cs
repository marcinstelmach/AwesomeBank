namespace AwesomeBank.BuildingBlocks.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AggregateRoot
    {
        private readonly ISet<IDomainEvent> _event = new HashSet<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => _event;

        public AggregateId Id { get; protected set; }

        public AggregateState State { get; protected set; }

        public int Version { get; protected set; }

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_event.Any())
            {
                Version++;
            }

            _event.Add(@event);
        }

        protected void ClearEvents() => _event.Clear();
    }
}