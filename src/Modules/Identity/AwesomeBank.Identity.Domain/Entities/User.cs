namespace AwesomeBank.Identity.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using AwesomeBank.BuildingBlocks.Domain;

    public class User : Entity, IAggregateRoot
    {
        private readonly List<ApplicationUserGroup> _applicationUserGroups;

        public User(string firstName, string lastName, string email, Password password, Role role)
            : this()
        {
            Id = new UserId(Guid.NewGuid());
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EmailConfirmed = false;
            Password = password;
            CreationDateTime = DateTimeOffset.UtcNow;
            IsDeleted = false;
            Role = role;
        }

        protected User()
        {
            _applicationUserGroups = new List<ApplicationUserGroup>();
        }

        public UserId Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public virtual Password Password { get; private set; }

        public DateTimeOffset CreationDateTime { get; private set; }

        public bool IsDeleted { get; private set; }

        public virtual Role Role { get; private set; }

        public virtual IReadOnlyCollection<ApplicationUserGroup> ApplicationUserGroups => _applicationUserGroups;
    }
}