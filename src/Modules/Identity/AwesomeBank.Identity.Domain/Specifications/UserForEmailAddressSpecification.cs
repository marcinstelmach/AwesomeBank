namespace AwesomeBank.Identity.Domain.Specifications
{
    using System;
    using System.Linq.Expressions;
    using AwesomeBank.BuildingBlocks.Domain.Specifications;
    using AwesomeBank.Identity.Domain.Entities;

    public class UserForEmailAddressSpecification : Specification<User>
    {
        private readonly string _email;

        public UserForEmailAddressSpecification(string email)
        {
            _email = email;
        }

        public override Expression<Func<User, bool>> ToExpression()
            => x => x.Email == _email;
    }
}