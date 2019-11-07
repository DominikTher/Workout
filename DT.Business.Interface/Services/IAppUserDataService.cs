using DT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Interface.Services
{
    public interface IAppUserDataService
    {
        public AppUser GetAppUser(string email, string password);
    }
}
