using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Catalogo.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddDbContext<ICatalogoDbContext, CatalogoDbContext>(opts =>
        {
            var connectionString = _configuration.GetConnectionString(nameof(CatalogoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(CatalogoDbContext).Assembly.FullName));
        });
        
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
    
    public static void MigrateDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<CatalogoDbContext>();
            context.Database.Migrate();
    }
}