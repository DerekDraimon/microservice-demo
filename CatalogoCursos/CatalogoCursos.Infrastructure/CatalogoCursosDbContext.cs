using CatalogoCursos.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogoCursos.Infrastructure;

public class CatalogoCursosDbContext : DbContext
{
    public CatalogoCursosDbContext(DbContextOptions<CatalogoCursosDbContext> options) : base(options)
    {
    }

    public DbSet<Curso> Cursos => Set<Curso>();
}
