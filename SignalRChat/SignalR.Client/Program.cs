using SignalR.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IChatHubConnection, ChatHubConnection>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();