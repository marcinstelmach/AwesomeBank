namespace AwesomeBank.Identity.Application.Exceptions
{
    using System;

    public abstract class IdentityBaseApplicationException : Exception
    {
        protected IdentityBaseApplicationException(string message)
            : base(message)
        {
        }

        public abstract string Code { get; }
    }
}