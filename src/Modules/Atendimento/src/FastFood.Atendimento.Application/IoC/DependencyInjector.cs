using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Atendimento.Application.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}