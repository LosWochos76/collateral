using System;
using Microsoft.AspNetCore.SignalR;
using SignalR.Shared;

namespace SignalR.Service;

public class ChatHub : Hub
{
    private ChatRepository repository;

    public ChatHub(ChatRepository repository)
    {
        this.repository = repository;
    }

    public async Task Register(User user)
    {
    }

    public async Task SendMessage(Message msg)
    {
    }
}
