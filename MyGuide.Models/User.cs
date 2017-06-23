using System;
using System.Security.Principal;

namespace MyGuide.Models
{
    public class User : IPrincipal
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public IIdentity Identity => new GenericIdentity(Username);

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
