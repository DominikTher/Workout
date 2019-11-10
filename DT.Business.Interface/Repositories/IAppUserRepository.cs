using DT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Interface.Repositories
{
    public interface IAppUserRepository
    {
        AppUser GetAppUser(string email);
        AppUser UpdateRefreshToken(int appUserId, string refreshToken);
    }
}
