using Matriculas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Matriculas.Infrastructure;

public class MatriculasDbContext : DbContext
{
    public MatriculasDbContext(DbContextOptions<MatriculasDbContext> options) : base(options) { }

    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
}
