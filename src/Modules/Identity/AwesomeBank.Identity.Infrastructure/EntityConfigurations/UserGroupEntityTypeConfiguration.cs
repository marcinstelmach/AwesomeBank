namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        private const string UserGroupsTableName = "UserGroups";
        private readonly string _schemaName;

        public UserGroupEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable(UserGroupsTableName, _schemaName);

            builder.HasKey(x => new { x.UserId, x.GroupId });
            builder.HasOne(x => x.Group)
                .WithOne();
        }
    }
}