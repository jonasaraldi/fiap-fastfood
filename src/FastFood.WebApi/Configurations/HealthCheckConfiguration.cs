using FastFood.Atendimento.Infrastructure.Persistence.Postgres.Contexts;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FastFood.WebApi.Configurations;

public static class HealthCheckConfiguration
{
    public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck("API", () => HealthCheckResult.Healthy(), tags: new[] { "api" });
        
        services.AddHealthChecks()
            .AddNpgSql(
                configuration.GetConnectionString(nameof(AtendimentoDbContext))!,
                name: $"Postgres ({nameof(AtendimentoDbContext)})",
                tags: new[] { "database", "db" } );
        
        services.AddHealthChecksUI(opt =>
            opt.SetEvaluationTimeInSeconds(15)
                .MaximumHistoryEntriesPerEndpoint(60)
                .SetApiMaxActiveRequests(1)
                .AddHealthCheckEndpoint("FastFood", "/health"))
            .AddInMemoryStorage();
    }
    
    public static void UseHealthCheck(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        app.UseHealthChecksUI(h => h.UIPath = "/health-ui");
    }
}