namespace AwesomeBank.Identity.Infrastructure.EntityConfigurations
{
    using AwesomeBank.Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        private const string RolesTableName = "Roles";
        private readonly string _schemaName;

        public RoleEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(RolesTableName, _schemaName);
            builder.HasKey(x => x.Id);
        }
    }
}