using SignalRPOC.Commons.Types;

namespace SignalRPOC.Commons.Interfaces;

public interface ISignalRImporter
{
    public event EventHandler<MessageModel>? OnMessage;
    public Task StartAsync(CancellationToken cancellationToken);
    public Task StopAsync();
    public Task SendMessageAsync(MessageModel message);
}