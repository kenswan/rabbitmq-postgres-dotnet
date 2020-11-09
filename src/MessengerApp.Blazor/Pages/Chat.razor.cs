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

        private void SendMessageEvent(MouseEventArgs e)
        {
            messageService.SendMessageAsync(new Message { Sender = User.Id, Body = currentMessage, Recipient = CurrentContact.Id });

            messages.Add(new Message { Sender = User.UserName, Body = currentMessage, Recipient = CurrentContact.UserName });

            currentMessage = "";

            StateHasChanged();
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
    }
}
