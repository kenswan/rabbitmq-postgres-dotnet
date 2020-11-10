using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerApp.Blazor.Models;
using MessengerApp.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MessengerApp.Blazor.Pages
{
    public partial class Contacts : ComponentBase
    {
        [Parameter]
        public Contact User { get; set; }

        [Parameter]
        public Action<Contact> SelectContact { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        private IEnumerable<Contact> contacts = Enumerable.Empty<Contact>();

        protected override async Task OnInitializedAsync()
        {
            contacts = await userService.GetContactsAsync(User.Id);
        }

        private void SelectContactOnClick(MouseEventArgs e, Contact contact)
        {
            SelectContact(contact);
        }
    }
}
