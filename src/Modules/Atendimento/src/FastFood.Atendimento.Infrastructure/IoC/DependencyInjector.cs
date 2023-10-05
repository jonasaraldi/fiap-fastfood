using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Atendimento.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddDbContext<IAtendimentoDbContext, AtendimentoDbContext>(opts =>
        {
            var connectionString = _configuration.GetConnectionString(nameof(AtendimentoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(AtendimentoDbContext).Assembly.FullName));
        });
        
        return services;
    }
    
    public static void MigrateDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<AtendimentoDbContext>();
            context.Database.Migrate();
    }
}