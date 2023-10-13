using FastFood.Atendimento.Application.IoC;
using FastFood.Atendimento.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Atendimento.Endpoints.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddAtendimentoModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        
        return services;
    }
    
    public static void UseAtendimentoModule(this WebApplication app)
    {
        app.MigrateDatabase();
    }
}