using FastFood.Catalogo.Application.Abstractions.UnitsOfWork;
using FastFood.Catalogo.Domain.Produtos.Repositories;
using FastFood.Catalogo.Infrastructure.Logging;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Contexts;
using FastFood.Catalogo.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using ILogger = FastFood.Catalogo.Application.Abstractions.ILogger;

namespace FastFood.Catalogo.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {   
        services.AddDbContext<ICatalogoDbContext, CatalogoDbContext>(opts =>
        {
            var connectionString = configuration.GetConnectionString(nameof(CatalogoDbContext));
            opts.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(CatalogoDbContext).Assembly.FullName));
        });

        services.AddSingleton<ILogger, SerialogLoggerAdapter>();
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        
        return services;
    }
    
    public static void UseInfrastructure(this WebApplication app)
    {
        app.ConfigureLogging();
        app.MigrateDatabase();
    }

    private static void MigrateDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<CatalogoDbContext>();
            context.Database.Migrate();
    }
    
    private static void ConfigureLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) =>
            {
                string[] ignorePaths = { "swagger", "health", "health-ui" };
                string path = httpContext.Request.Path.ToString();

                bool existsPathsToIgnore = ignorePaths.Any(ignorePath => path.Contains(ignorePath));
                if (existsPathsToIgnore) return LogEventLevel.Debug;
                    
                int statusCode = httpContext.Response.StatusCode;
                return statusCode switch
                {
                    >= 500 => LogEventLevel.Fatal,
                    >= 400 => LogEventLevel.Error,
                    >= 300 => LogEventLevel.Warning,
                    >= 200 => LogEventLevel.Information,
                    _ => LogEventLevel.Information
                };
            };
        });
    }
}