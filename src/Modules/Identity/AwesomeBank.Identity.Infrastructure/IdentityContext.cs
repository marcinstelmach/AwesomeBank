namespace AwesomeBank.Identity.Infrastructure
{
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Infrastructure.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class IdentityContext : DbContext, IUnitOfWork
    {
        private const string IdentitySchemaName = "Identity";

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(IdentitySchemaName));
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration(IdentitySchemaName));
            modelBuilder.ApplyConfiguration(new ApplicationUserGroupEntityTypeConfiguration(IdentitySchemaName));
            modelBuilder.ApplyConfiguration(new ApplicationGroupEntityTypeConfiguration(IdentitySchemaName));
            modelBuilder.ApplyConfiguration(new ApplicationGroupClaimEntityTypeConfiguration(IdentitySchemaName));
            modelBuilder.ApplyConfiguration(new ClaimsEntityTypeConfiguration(IdentitySchemaName));
        }
    }
}