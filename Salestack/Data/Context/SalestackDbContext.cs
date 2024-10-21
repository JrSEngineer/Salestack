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

        public DbSet<SalestackCompany> Company { get; set; }
        public DbSet<SalestackSeller> Seller {  get; set; }
        public DbSet<SalestackManager> Manager {  get; set; }
        public DbSet<SalestackDirector> Director {  get; set; }
    }
}
