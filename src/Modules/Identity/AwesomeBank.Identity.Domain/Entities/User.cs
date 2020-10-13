namespace AwesomeBank.Identity.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Exceptions;
    using AwesomeBank.Identity.Domain.ValueObjects;

    public class User : Entity, IAggregateRoot
    {
        private readonly List<UserGroup> _userGroups;

        public User(string firstName, string lastName, string email, Password password, DateTime birthDayDate, IdentityDocument identityDocument)
            : this()
        {
            Id = new UserId(Guid.NewGuid());
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EmailConfirmed = false;
            Password = password;
            SetBirthDayDate(birthDayDate);
            IdentityDocument = identityDocument;
            CreationDateTime = DateTimeOffset.UtcNow;
            IsDeleted = false;
        }

        protected User()
        {
            _userGroups = new List<UserGroup>();
        }

        public UserId Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public DateTime BirthDayDate { get; private set; }

        public virtual IdentityDocument IdentityDocument { get; private set; }

        public virtual Password Password { get; private set; }

        public DateTimeOffset CreationDateTime { get; private set; }

        public bool IsDeleted { get; private set; }

        public virtual IReadOnlyCollection<UserGroup> UserGroups => _userGroups;

        public void SetBirthDayDate(DateTime birthDayDate)
        {
            if (birthDayDate.AddYears(18) > DateTime.UtcNow)
            {
                throw new UserTooYoungException(Id, 18);
            }

            BirthDayDate = birthDayDate;
        }

        public void AssignToGroup(Group group)
        {
            Insist.IsNotNull(group, nameof(group));
            _userGroups.Add(new UserGroup(Id, group.Id));
        }
    }
}