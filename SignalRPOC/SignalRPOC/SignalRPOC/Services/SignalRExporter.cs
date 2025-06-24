using Microsoft.AspNetCore.SignalR;
using SignalRPOC.Commons.Configurations;
using SignalRPOC.Commons.Interfaces;
using SignalRPOC.Commons.Types;

namespace SignalRPOC.Commons.Services;

public class SignalRExporter : Hub, ISignalRExporter
{
    public async Task SendMessage(MessageModel message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}