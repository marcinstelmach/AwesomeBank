namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject<TValueObject> : IEquatable<ValueObject<TValueObject>>
        where TValueObject : class
    {
        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        public bool Equals(ValueObject<TValueObject> other)
        {
            if (other == null)
            {
                return false;
            }

            using (var thisValues = GetAtomicValues().GetEnumerator())
            {
                using (var otherValues = other.GetAtomicValues().GetEnumerator())
                {
                    while (thisValues.MoveNext() && otherValues.MoveNext())
                    {
                        if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                        {
                            return false;
                        }

                        if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                        {
                            return false;
                        }
                    }

                    return !thisValues.MoveNext() && !otherValues.MoveNext();
                }
            }
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return other is ValueObject<TValueObject> valueObject && Equals(valueObject);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public virtual TValueObject GetCopy()
        {
            return (TValueObject)MemberwiseClone();
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}