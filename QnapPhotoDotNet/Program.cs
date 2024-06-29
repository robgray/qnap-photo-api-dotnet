using Microsoft.Extensions.Options;

using RobGray.QnapPhotoDotNet.Core.QnapApi;
using RobGray.QnapPhotoDotNet.Infrastructure.DelegatingHandlers;
using RobGray.QnapPhotoDotNet.Infrastructure.Middleware;

namespace RobGray.QnapPhotoDotNet;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets<QnapApiOptions>();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddOptions<QnapApiOptions>()
            .Bind(builder.Configuration.GetSection(QnapApiOptions.Key))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services
            .AddHttpClient(QnapApiClient.HttpClientKey, (provider, client) =>
            {
                var settings = provider.GetRequiredService<IOptionsMonitor<QnapApiOptions>>().CurrentValue;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddHttpMessageHandler(provider =>
            {
                return new AuthenticationHandler(provider.GetRequiredService<IOptionsMonitor<QnapApiOptions>>().CurrentValue);
            });

        builder.Services.AddScoped<IQnapApiClient, QnapApiClient>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<QnapImageMiddleware>();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}