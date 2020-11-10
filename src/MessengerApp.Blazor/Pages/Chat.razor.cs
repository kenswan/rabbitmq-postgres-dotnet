using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;
using MessengerApp.Blazor.Providers;
using MessengerApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;

namespace MessengerApp.Blazor.Pages
{
    public partial class Chat : ComponentBase
    {
        [Parameter]
        public Contact User { get; set; }

        [Parameter]
        public Contact CurrentContact { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        private IMessageService messageService { get; set; }

        private List<Message> messages = new List<Message>();

        private string currentMessage = "";

        private string hubUrl;
        private HubConnection hubConnection;

        protected override async Task OnParametersSetAsync()
        {
            // Refresh messages when params passed to component (new current contact)
            await RefreshMessages();
        }

        protected override async Task OnInitializedAsync()
        {
            string baseUrl = navigationManager.BaseUri;

            hubUrl = baseUrl.TrimEnd('/') + MessengerAppHubProvider.HubUrl;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            hubConnection.On<string, Message>("Broadcast", BroadcastMessage);

            await hubConnection.StartAsync();
        }

        private void BroadcastMessage(string userId, Message message)
        {
            if (userId.Equals(User.Id, StringComparison.OrdinalIgnoreCase))
            {
                messages.Add(message);


                StateHasChanged();
            }
        }

        private async Task SendMessageEvent(MouseEventArgs e)
        {
            var message = await messageService.SendMessageAsync(User.Id, CurrentContact.Id, currentMessage);

            if (message != null)
            {
                messages.Add(message);

                await hubConnection.SendAsync("Broadcast", CurrentContact.Id, message);

                currentMessage = "";

                StateHasChanged();
            }
        }

        private async Task RefreshMessagesEvent(MouseEventArgs e)
        {
            await RefreshMessages();
        }

        private async Task RefreshMessages()
        {
            var foundMessages = await messageService.GetMessagesAsync(User.Id, CurrentContact.Id);

            messages = foundMessages.ToList();
        }

        private string GetUserName(string userId)
        {
            switch (userId)
            {
                case { } when (userId == User.Id):
                    return User.UserName;
                case { } when (userId == CurrentContact.Id):
                    return CurrentContact.UserName;
                default:
                    return "Unknown";
            }
        }
    }
}
