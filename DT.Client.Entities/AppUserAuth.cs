using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Client.Entities
{
    public class AppUserAuth
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }

        public AppUserAuth()
        {
            Name = "Not authorized";
            Token = string.Empty;
        }
    }
}
