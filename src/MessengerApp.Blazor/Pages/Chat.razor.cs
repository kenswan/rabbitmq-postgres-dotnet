using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;
using MessengerApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MessengerApp.Blazor.Pages
{
    public partial class Chat : ComponentBase
    {
        [Parameter]
        public Contact User { get; set; }

        [Parameter]
        public Contact CurrentContact { get; set; }

        [Inject]
        private IMessageService messageService { get; set; }

        private List<Message> messages = new List<Message>();

        private string currentMessage = "";

        protected override async Task OnInitializedAsync()
        {
            await RefreshMessages();
        }

        private async Task SendMessageEvent(MouseEventArgs e)
        {
            var message = await messageService.SendMessageAsync(User.Id, CurrentContact.Id, currentMessage);

            if (message != null)
            {
                messages.Add(message);

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
