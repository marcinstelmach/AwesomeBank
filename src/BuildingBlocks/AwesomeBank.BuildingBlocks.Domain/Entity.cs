namespace AwesomeBank.BuildingBlocks.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Entity
    {
        private readonly ISet<IDomainEvent> _event = new HashSet<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => _event;

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