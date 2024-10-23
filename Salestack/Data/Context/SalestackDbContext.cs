using Microsoft.EntityFrameworkCore;
using Salestack.Entities.Company;
using Salestack.Entities.Users;

namespace Salestack.Data.Context
{
    public class SalestackDbContext : DbContext
    {
        public SalestackDbContext(DbContextOptions<SalestackDbContext> options) : base(options)
        {
            bool pendingMigrationsFound = this.Database.GetPendingMigrations().Any();

            if (pendingMigrationsFound) this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SalestackDirector>().HasIndex(d => d.CompanyId).IsUnique();

            builder.Entity<SalestackManager>().HasIndex(d => d.CompanyId).IsUnique();

            builder.Entity<SalestackSeller>().HasIndex(d => d.CompanyId).IsUnique();
        }

        public DbSet<SalestackCompany> Company { get; set; }
        public DbSet<SalestackSeller> Seller { get; set; }
        public DbSet<SalestackManager> Manager { get; set; }
        public DbSet<SalestackDirector> Director { get; set; }
    }
}
