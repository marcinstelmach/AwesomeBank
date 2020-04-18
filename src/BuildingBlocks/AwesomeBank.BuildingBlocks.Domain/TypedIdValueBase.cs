namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;
    using System.Collections.Generic;

    public abstract class TypedIdValueBase<T> : IEquatable<TypedIdValueBase<T>>
    {
        protected TypedIdValueBase(T value)
        {
            Value = value;
        }

        public T Value { get; }

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

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((TypedIdValueBase<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public bool Equals(TypedIdValueBase<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}