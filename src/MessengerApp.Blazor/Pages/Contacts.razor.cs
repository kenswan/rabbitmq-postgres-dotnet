using System;
using System.Collections.Generic;
using MessengerApp.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MessengerApp.Blazor.Pages
{
    public partial class Contacts : ComponentBase
    {
        [Parameter]
        public Contact User { get; set; }

        [Parameter]
        public IEnumerable<Contact> ContactList { get; set; }

        [Parameter]
        public Action<Contact> SelectContact { get; set; }

        private void SelectContactOnClick(MouseEventArgs e, Contact contact)
        {
            SelectContact(contact);
        }
    }
}
