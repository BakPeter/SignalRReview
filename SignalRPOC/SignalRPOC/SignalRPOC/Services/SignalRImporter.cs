using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using SignalRPOC.Commons.Configurations;
using SignalRPOC.Commons.Interfaces;
using SignalRPOC.Commons.Types;

namespace SignalRPOC.Commons.Services;

public class SignalRImporter : ISignalRImporter
{
    public event EventHandler<MessageModel>? OnMessage;

    private readonly ILogger<SignalRImporter> _logger;
    private readonly SignalRConfigurations _settings;
    private readonly HubConnection _connection;

    public SignalRImporter(ILogger<SignalRImporter> logger, SignalRConfigurations settings)
    {
        _logger = logger;
        _settings = settings;

        _connection = new HubConnectionBuilder()
            .WithUrl(_settings.Url, HttpTransportType.WebSockets)
            .WithAutomaticReconnect()
            .Build();
        _connection.On<MessageModel>("ReceiveMessage", model => OnMessage?.Invoke(this, model));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.StartAsync(cancellationToken);
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
    }

    public async Task SendMessageAsync(MessageModel message)
    {
        if (_connection.State != HubConnectionState.Connected)
            await StartAsync(CancellationToken.None);
        await _connection.SendAsync("SendMessage", message);
    }
}