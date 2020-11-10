using System.Collections.Generic;
using System.Linq;
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
        private string NewContact { get; set; }
        private Contact currentUser = null;
        private Contact selectedContact = null;
        private List<Contact> contacts = new List<Contact>();

        private async Task LoginUserOnClick(MouseEventArgs e)
        {
            currentUser = await userService.LogInUserByUserNameAsync(Username);

            var userContacts = await userService.GetContactsAsync(currentUser.Id);

            contacts = userContacts?.ToList();
        }

        private void GetMessagesForContactById(Contact contact)
        {
            selectedContact = contact;

            StateHasChanged();
        }

        private async Task AddContactOnClick(MouseEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewContact))
            {
                var newContact = await userService.GetUserAsync(NewContact);

                if (newContact != null)
                {
                    if (!contacts.Contains(newContact))
                    {
                        contacts.Add(newContact);
                    }

                    NewContact = "";

                    GetMessagesForContactById(newContact);
                }
            }
        }
    }
}
