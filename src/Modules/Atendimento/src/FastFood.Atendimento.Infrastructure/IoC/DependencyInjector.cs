using FastFood.Atendimento.Application.Abstractions.UnitsOfWork;
using FastFood.Atendimento.Domain.Pedidos.Repositories;
using FastFood.Atendimento.Infrastructure.Logging;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;
using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using ILogger = FastFood.Atendimento.Application.Abstractions.ILogger;

namespace FastFood.Atendimento.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddDbContext<IAtendimentoDbContext, AtendimentoDbContext>(opts =>
        {
            var connectionString = configuration.GetConnectionString(nameof(AtendimentoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(AtendimentoDbContext).Assembly.FullName));
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
        using var context = serviceScope.ServiceProvider.GetService<AtendimentoDbContext>();
        context.Database.Migrate();
    }
}