namespace AwesomeBank.Identity.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AwesomeBank.BuildingBlocks.Domain;

    public class User : AggregateRoot
    {
        private readonly List<ApplicationUserGroup> _applicationUserGroups;

        public User(string firstName, string lastName, string email, Password password, Role role)
            : this()
        {
            Id = new AggregateId();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EmailConfirmed = false;
            Password = password;
            CreationDateTime = DateTimeOffset.UtcNow;
            IsDeleted = false;
            Role = role;
        }

        private User()
        {
            _applicationUserGroups = new List<ApplicationUserGroup>();
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public virtual Password Password { get; private set; }

        public DateTimeOffset CreationDateTime { get; private set; }

        public bool IsDeleted { get; private set; }

        public virtual Role Role { get; private set; }

        public virtual IReadOnlyCollection<Claim> Claims => _applicationUserGroups
            .SelectMany(x => x.ApplicationGroups)
            .SelectMany(x => x.ApplicationGroupClaims)
            .Select(x => x.Claim)
            .ToList();
    }
}