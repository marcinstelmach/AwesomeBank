namespace AwesomeBank.Identity.Domain.Entities
{
    using System;
    using AwesomeBank.BuildingBlocks.Domain;

    public class UserId : TypedIdValueBase<Guid>
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}