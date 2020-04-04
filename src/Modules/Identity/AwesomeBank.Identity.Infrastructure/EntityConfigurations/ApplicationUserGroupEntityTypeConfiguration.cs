namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserGroupEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUserGroup>
    {
        private const string ApplicationUserGroupsTableName = "ApplicationUserGroups";
        private readonly string _schemaName;

        public ApplicationUserGroupEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<ApplicationUserGroup> builder)
        {
            builder.ToTable(ApplicationUserGroupsTableName, _schemaName);

            builder.HasKey(x => new { x.UserId, x.ApplicationGroupId });
            builder.HasOne(x => x.ApplicationGroup)
                .WithOne();
        }
    }
}