using DT.Client.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Interface.Services
{
    public interface IAppUserDataService
    {
        AppUser GetAppUser(string email);
        AppUser GetAppUser(string email, string password);
        AppUser UpdateRefreshToken(int appUserId, string refreshToken);
        Task<AppUser> AddAsync(AppUser appUser);
    }
}
