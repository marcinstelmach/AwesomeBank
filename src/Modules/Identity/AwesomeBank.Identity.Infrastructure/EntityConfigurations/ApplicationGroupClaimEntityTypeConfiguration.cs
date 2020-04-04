namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationGroupClaimEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationGroupClaim>
    {
        private const string ApplicationGroupClaimsTableName = "ApplicationGroupClaims";
        private readonly string _schemaName;

        public ApplicationGroupClaimEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<ApplicationGroupClaim> builder)
        {
            builder.ToTable(ApplicationGroupClaimsTableName, _schemaName);

            builder.HasKey(x => new { x.ApplicationGroupId, x.ClaimId });
            builder.HasOne(x => x.Claim)
                .WithMany()
                .HasForeignKey(x => x.ClaimId);
        }
    }
}