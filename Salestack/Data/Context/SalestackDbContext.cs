using Microsoft.EntityFrameworkCore;
using Salestack.Entities.Company;
using Salestack.Entities.Teams;
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
            builder.Entity<SalestackCompany>().HasIndex(c => c.Cnpj).IsUnique();

            builder.Entity<SalestackCompany>().HasIndex(c => c.PhoneNumber).IsUnique();

            builder.Entity<SalestackDirector>().HasIndex(d => d.CompanyId).IsUnique();

            builder.Entity<SalestackCompany>().HasMany(c => c.Managers).WithOne(m => m.Company);

            builder.Entity<SalestackCompany>().HasMany(c => c.Sellers).WithOne(m => m.Company);

            builder.Entity<SalestackTeam>().HasOne(t => t.Company).WithMany(c => c.Teams);
        }

        public DbSet<SalestackCompany> Company { get; set; }
        public DbSet<SalestackDirector> Director { get; set; }
        public DbSet<SalestackManager> Manager { get; set; }
        public DbSet<SalestackSeller> Seller { get; set; }
        public DbSet<SalestackTeam> Team { get; set; }
    }
}
