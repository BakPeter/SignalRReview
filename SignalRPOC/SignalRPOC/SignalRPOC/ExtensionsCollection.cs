using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SignalRPOC.Commons.Configurations;
using SignalRPOC.Commons.Interfaces;
using SignalRPOC.Commons.Services;

namespace SignalRPOC.Commons;

public static class ExtensionsCollection
{
    public static void RegisterSignalRServices(this IServiceCollection services, SignalRConfigurations settings)
    {
        services.AddSingleton(settings);
        services.AddSingleton<ISignalRExporter, SignalRExporter>();
        services.AddSingleton<ISignalRImporter, SignalRImporter>();

        services.AddSignalR();
    }

    public static void MapSignalRHubs(this WebApplication app, SignalRConfigurations settings)
    {
        app.MapHub<SignalRExporter>($"/datamodel");
    }
}