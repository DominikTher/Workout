using DT.Business.Entities;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Services
{
    public class AppUserDataService : BaseDataService, IAppUserDataService
    {
        private readonly IAppUserRepository appUserRepository;

        public AppUserDataService(IAppUserRepository appUserRepository)
            : base(appUserRepository as IEntityRepository, null) => this.appUserRepository = appUserRepository;

        public AppUser GetAppUser(string email, string password)
        {
            return appUserRepository.GetAppUser(email, password);
        }
    }
}
