using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Atendimento.Application.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        return services;
    }
}