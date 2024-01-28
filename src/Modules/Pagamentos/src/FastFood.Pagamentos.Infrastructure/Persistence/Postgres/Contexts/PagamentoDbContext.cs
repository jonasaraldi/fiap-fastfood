using FastFood.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Contexts;

public interface IPagamentoDbContext
{
    DbSet<Pagamento> Pagamentos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

public class PagamentoDbContext : DbContext, IPagamentoDbContext
{
    private readonly IConfiguration _configuration;

    public PagamentoDbContext(DbContextOptions<PagamentoDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Pagamento> Pagamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString(GetType().Name));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}