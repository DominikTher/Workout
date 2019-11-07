

using DT.Client.Entities;

namespace DT.Business.Interface.Authentication
{
    public interface IAuthenticationService
    {
        public AppUserAuth ValidateUser(AppUser appUser);
    }
}
