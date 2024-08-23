using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ComicsShop.Identity.Api.Configurations;

public static class OpenTelemetryConfigurations
{
    public static void OpenTelemetryConfigure(this WebApplicationBuilder builder)
    {
        const string serviceName = "comics-shop-identity";
        
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => 
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddNpgsql()
                    .AddOtlpExporter()
                    .AddConsoleExporter()
            )
            .WithMetrics(metrics => 
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
            );
        
        builder.Logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddOtlpExporter()
                .AddConsoleExporter();
        });
    }
}