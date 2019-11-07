using DT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Interface.Repositories
{
    public interface IAppUserRepository
    {
        public AppUser GetAppUser(string email, string password);
    }
}
