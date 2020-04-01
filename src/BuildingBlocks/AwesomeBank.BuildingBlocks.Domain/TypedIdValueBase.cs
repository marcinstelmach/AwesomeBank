namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;

    public abstract class TypedIdValueBase<T> : IEquatable<TypedIdValueBase<T>>
    {
        protected TypedIdValueBase(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidOperationException("Id value cannot be empty!");
            }

            Value = value;
        }

        public Guid Value { get; }

        public static bool operator ==(TypedIdValueBase<T> obj1, TypedIdValueBase<T> obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }

                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypedIdValueBase<T> x, TypedIdValueBase<T> y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is TypedIdValueBase<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(TypedIdValueBase<T> other)
        {
            return this.Value == other?.Value;
        }
    }
}