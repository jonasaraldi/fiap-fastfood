using FastFood.Pagamentos.Application.Gateways.Logging;
using FastFood.Pagamentos.Application.Gateways.Repositories;
using FastFood.Pagamentos.Application.Gateways.UnitOfWorks;
using FastFood.Pagamentos.Infrastructure.Logging;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Contexts;
using FastFood.Pagamentos.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ILogger = FastFood.Pagamentos.Application.Gateways.Logging.ILogger;

namespace FastFood.Pagamentos.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddDbContext<IPagamentoDbContext, PagamentoDbContext>(opts =>
        {
            var connectionString = configuration.GetConnectionString(nameof(PagamentoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(PagamentoDbContext).Assembly.FullName));
        });
        
        services.AddSingleton<ILogger, SerialogLogger>();
        services.AddTransient<IPagamentoRepository, PagamentoRepository>();
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
        using var context = serviceScope.ServiceProvider.GetService<PagamentoDbContext>();
        context.Database.Migrate();
    }
}