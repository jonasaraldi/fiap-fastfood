using FastFood.Catalogo.Application.IoC;
using FastFood.Catalogo.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Catalogo.Endpoints.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddCatalogoModule(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddInfrastructure(configuration, builder);
        services.AddApplication();
        
        return services;
    }
    
    public static void UseCatalogoModule(this WebApplication app)
    {
        app.UseInfrastructure();
    }
}