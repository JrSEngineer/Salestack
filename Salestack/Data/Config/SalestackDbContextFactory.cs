using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;

namespace Salestack.Data.Config;

public class SalestackDbContextFactory : IDbContextFactory<SalestackDbContext>
{
    private readonly DbContextOptions<SalestackDbContext> _options;
    public SalestackDbContextFactory(DbContextOptions<SalestackDbContext> options)
    {
        _options = options;
    }

    public SalestackDbContext CreateDbContext()
    {
        return new SalestackDbContext(_options);
    }
}
