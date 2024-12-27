namespace RobGray.QnapPhotoApiDotNet;

using DelegatingHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Middleware;
using QnapApi;

public static class Configuration
{
    public static IServiceCollection AddQnapPhotoApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<QnapApiOptions>()
            .Bind(configuration.GetSection(QnapApiOptions.Key))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddMemoryCache();
        
        services.TryAddSingleton(TimeProvider.System);
        
        services
            .AddHttpClient(PhotoStationClient.HttpClientKey, (provider, client) =>
            {
                var settings = provider.GetRequiredService<IOptionsMonitor<QnapApiOptions>>().CurrentValue;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddHttpMessageHandler(provider => new AuthenticationHandler(
                provider.GetRequiredService<IOptionsMonitor<QnapApiOptions>>().CurrentValue,
                provider.GetRequiredService<ILogger>(), 
                provider.GetRequiredService<IMemoryCache>(),
                provider.GetRequiredService<TimeProvider>()));

        services.AddScoped<IPhotoStationClient, PhotoStationClient>();
        
        return services;
    }
    
    public static WebApplication UseQnapImageMiddleware(this WebApplication app)
    {
        app.UseMiddleware<QnapImageMiddleware>();
        return app;
    }
}