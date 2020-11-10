using System;
using System.Collections.Generic;
using MessengerApp.Api.Models;

namespace MessengerApp.Api.Services
{
    internal class UserComparer : EqualityComparer<User>
    {
        public override bool Equals(User x, User y)
        {
            return x.Id == y.Id;
        }

        public override int GetHashCode(User user)
        {
            return user == null ? 0 : user.Id.GetHashCode();
        }
    }
}
