using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Pagamentos.Endpoints.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddPagamentoModule(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        // services.AddInfrastructure(configuration, builder);
        // services.AddApplication();
        
        return services;
    }
    
    public static void UsePagamentoModule(this WebApplication app)
    {
        // app.UseInfrastructure();
    }
}