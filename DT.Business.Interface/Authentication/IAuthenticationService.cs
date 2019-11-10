

using DT.Client.Entities;

namespace DT.Business.Interface.Authentication
{
    public interface IAuthenticationService
    {
        AppUserAuth ValidateUser(AppUser appUser);
        AppUserAuth Refresh(string token, string refreshToken);
    }
}
