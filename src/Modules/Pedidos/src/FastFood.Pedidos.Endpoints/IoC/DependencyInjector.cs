using FastFood.Pedidos.Application.IoC;
using FastFood.Pedidos.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Pedidos.Endpoints.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddPedidoModule(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddInfrastructure(configuration, builder);
        services.AddApplication();
        
        return services;
    }
    
    public static void UsePedidoModule(this WebApplication app)
    {
        app.UseInfrastructure();
    }
}