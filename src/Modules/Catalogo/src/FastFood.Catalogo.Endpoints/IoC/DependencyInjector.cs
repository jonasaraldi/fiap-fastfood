using FastFood.Catalogo.Application.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Catalogo.Endpoints.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddCatalogoModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDependencies();
        
        return services;
    }
    
    public static void UseCatalogoModule(this WebApplication app)
    {
        //app.MigrateDatabase();
    }
}