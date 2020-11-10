using System;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace MessengerApp.Blazor.Providers
{
    public class MessengerAppHubProvider : Hub
    {
        public const string HubUrl = "/chat";

        public async Task Broadcast(string userId, Message message)
        {
            await Clients.All.SendAsync("Broadcast", userId, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
