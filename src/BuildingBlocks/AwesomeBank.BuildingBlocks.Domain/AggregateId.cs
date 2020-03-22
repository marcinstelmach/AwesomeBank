namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;

    public class AggregateId : IEquatable<AggregateId>
    {
        public AggregateId()
            : this(Guid.NewGuid())
        {
        }

        public AggregateId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static implicit operator Guid(AggregateId id) => id.Value;

        public static implicit operator AggregateId(Guid id) => new AggregateId(id);

        public bool Equals(AggregateId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AggregateId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString() => Value.ToString();
    }
}