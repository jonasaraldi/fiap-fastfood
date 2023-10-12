using FastFood.Catalogo.Domain.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;

public interface ICatalogoDbContext
{
    DbSet<Produto> Produtos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

public class CatalogoDbContext : DbContext, ICatalogoDbContext
{
    private readonly IConfiguration _configuration;

    public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Produto> Produtos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString(GetType().Name));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}