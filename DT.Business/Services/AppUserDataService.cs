using AutoMapper;
using DT.Business.Interface.Authentication;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using System.Threading.Tasks;
using AppUserBusiness = DT.Business.Entities.AppUser;

namespace DT.Business.Services
{
    public class AppUserDataService : BaseDataService, IAppUserDataService
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;

        public AppUserDataService(IAppUserRepository appUserRepository, IPasswordHasher passwordHasher, IMapper mapper)
            : base(appUserRepository as IEntityRepository, mapper) => (this.appUserRepository, this.passwordHasher, this.mapper) = (appUserRepository, passwordHasher, mapper);

        public AppUser GetAppUser(string email, string password)
        {
            var appUser = appUserRepository.GetAppUser(email);

            return (passwordHasher.Check(appUser.Password, password).Verified == true) ? mapper.Map<AppUser>(appUser) : null;
        }

        public AppUser GetAppUser(string email)
        {
            return mapper.Map<AppUser>(appUserRepository.GetAppUser(email));
        }

        public AppUser UpdateRefreshToken(int appUserId, string refreshToken)
        {
            return mapper.Map<AppUser>(appUserRepository.UpdateRefreshToken(appUserId, refreshToken));
        }

        public async Task<AppUser> AddAsync(AppUser appUser)
        {
            appUser.Password = passwordHasher.Hash(appUser.Password);

            return await AddAsync<AppUser, AppUserBusiness>(appUser);
        }
    }
}
