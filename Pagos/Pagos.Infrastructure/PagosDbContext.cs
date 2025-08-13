using Microsoft.EntityFrameworkCore;
using Pagos.Domain;

namespace Pagos.Infrastructure;

public class PagosDbContext : DbContext
{
    public PagosDbContext(DbContextOptions<PagosDbContext> options) : base(options) { }

    public DbSet<Pago> Pagos => Set<Pago>();
}
