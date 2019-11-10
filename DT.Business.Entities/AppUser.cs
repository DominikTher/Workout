using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
