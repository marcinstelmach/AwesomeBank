namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;

    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {
        }

        public abstract string Code { get; }
    }
}