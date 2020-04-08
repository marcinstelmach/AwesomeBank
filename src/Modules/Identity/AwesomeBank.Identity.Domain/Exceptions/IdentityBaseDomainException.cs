namespace AwesomeBank.Identity.Domain.Exceptions
{
    using System;

    public abstract class IdentityBaseDomainException : Exception
    {
        protected IdentityBaseDomainException(string message)
            : base(message)
        {
        }

        public abstract string Code { get; }
    }
}