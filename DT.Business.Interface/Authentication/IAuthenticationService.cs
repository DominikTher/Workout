

using DT.Client.Entities;
using System.Threading.Tasks;

namespace DT.Business.Interface.Authentication
{
    public interface IAuthenticationService
    {
        Task<AppUser> RegisterAsnyc(AppUser appUser);
        AppUserAuth ValidateUser(AppUser appUser);
        Task<AppUserAuth> ValidateExternalUser(AppUser appUser);
        AppUserAuth Refresh(string token, string refreshToken);
    }
}
