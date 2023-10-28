using FastFood.Pedidos.Domain.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FastFood.Pedidos.Infrastructure.Persistence.Postgres.Contexts;

public interface IPedidoDbContext
{
    DbSet<Pedido> Pedidos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

public class PedidoDbContext : DbContext, IPedidoDbContext
{
    private readonly IConfiguration _configuration;

    public PedidoDbContext(DbContextOptions<PedidoDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString(GetType().Name));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}