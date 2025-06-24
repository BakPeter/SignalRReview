using SignalRPOC.Client1;
using SignalRPOC.Commons;
using SignalRPOC.Commons.Configurations;

var builder = Host.CreateApplicationBuilder(args);

var settings = builder.Configuration.GetSection("SignalRConfigurations").Get<SignalRConfigurations>();
if (settings is null)
    throw new ArgumentNullException(nameof(settings), "Section \'SignalRConfigurations\' is not in appsettings.json");
builder.Services.RegisterSignalRServices(settings);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();