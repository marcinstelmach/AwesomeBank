namespace AwesomeBank.BuildingBlocks.Domain
{
    using System;
    using System.Net;

    public abstract class ApplicationBaseException : Exception
    {
        protected ApplicationBaseException(string message)
            : base(message)
        {
        }

        public abstract string Code { get; }

        public abstract HttpStatusCode StatusCode { get; }
    }
}