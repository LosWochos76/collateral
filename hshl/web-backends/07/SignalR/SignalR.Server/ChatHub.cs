using System;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server;

public class ChatHub : Hub
{
    public async Task SendMessage(Message msg)
    {
        IClientProxy proxy = Clients.All;
        await proxy.SendAsync("ReceiveMessage", msg);
    }
}
