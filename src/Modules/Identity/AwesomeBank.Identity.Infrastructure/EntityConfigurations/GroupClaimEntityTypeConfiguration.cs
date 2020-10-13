namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GroupClaimEntityTypeConfiguration : IEntityTypeConfiguration<GroupClaim>
    {
        private const string ApplicationGroupClaimsTableName = "GroupClaims";
        private readonly string _schemaName;

        public GroupClaimEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<GroupClaim> builder)
        {
            builder.ToTable(ApplicationGroupClaimsTableName, _schemaName);

            builder.Property<int>("Id");
            builder.HasKey("Id");
        }
    }
}