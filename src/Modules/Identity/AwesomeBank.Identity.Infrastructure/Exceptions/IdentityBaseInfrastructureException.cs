namespace AwesomeBank.Identity.Infrastructure.Exceptions
{
    using System;

    public abstract class IdentityBaseInfrastructureException : Exception
    {
        protected IdentityBaseInfrastructureException(string message)
            : base(message)
        {
        }

        public abstract string Code { get; }
    }
}