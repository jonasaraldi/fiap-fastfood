using FastFood.Atendimento.Application.Abstractions;
using FastFood.Atendimento.Domain.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;

public interface IAtendimentoDbContext
{
    DbSet<Pedido> Pedidos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}

public class AtendimentoDbContext : DbContext, IAtendimentoDbContext
{
    private readonly IConfiguration _configuration;

    public AtendimentoDbContext(DbContextOptions<AtendimentoDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString(GetType().Name));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtendimentoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}