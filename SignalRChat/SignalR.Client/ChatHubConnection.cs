using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Client;

public interface IChatHubConnection
{
}

public class ChatHubConnection : IChatHubConnection
{
    private readonly ILogger<ChatHubConnection> _logger;
    private readonly HubConnection _connection;

    public ChatHubConnection(ILogger<ChatHubConnection> logger)
    {
        _logger = logger;

        _connection = new HubConnectionBuilder().WithUrl("https://localhost:7070/chathub").Build();
        _connection.On<string, string>("ReceiveMessage", (s, s1) => { OnMessage(s, s1); });

        _ = StartAsync();
    }

    private async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    private Task OnMessage(string user, string message)
    {
        _logger.LogInformation($"{user} says {message}");
        return Task.CompletedTask;
    }
}