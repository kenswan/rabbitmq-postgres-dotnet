using System.Threading.Tasks;
using MessengerApp.Blazor.Models;
using MessengerApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MessengerApp.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IUserService userService { get; set; }

        private string Username { get; set; }

        private Contact currentUser = null;

        private Contact selectedContact = null;

        private async Task LoginUser(MouseEventArgs e)
        {
            currentUser = await userService.LogInUserByUserNameAsync(Username);
        }

        private void GetMessagesForContactById(Contact contact)
        {
            selectedContact = contact;

            StateHasChanged();
        }
    }
}
