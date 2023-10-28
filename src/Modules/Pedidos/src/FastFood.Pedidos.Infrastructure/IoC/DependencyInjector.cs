using FastFood.Pedidos.Application.Abstractions.UnitsOfWork;
using FastFood.Pedidos.Domain.Pedidos.Repositories;
using FastFood.Pedidos.Infrastructure.Logging;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Contexts;
using FastFood.Pedidos.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ILogger = FastFood.Pedidos.Application.Abstractions.ILogger;

namespace FastFood.Pedidos.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddDbContext<IPedidoDbContext, PedidoDbContext>(opts =>
        {
            var connectionString = configuration.GetConnectionString(nameof(PedidoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(PedidoDbContext).Assembly.FullName));
        });
        
        services.AddSingleton<ILogger, SerialogLoggerAdapter>();
        services.AddTransient<IPedidoRespository, PedidoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        
        return services;
    }
    
    public static void UseInfrastructure(this WebApplication app)
    {
        app.MigrateDatabase();
    }
    
    private static void MigrateDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<PedidoDbContext>();
            context.Database.Migrate();
    }
}