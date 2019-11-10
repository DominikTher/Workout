using DT.Business.Entities;
using DT.Business.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DT.DataRepository.Repositories
{
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        private readonly Func<WorkoutContext> contextFactory;

        public AppUserRepository(Func<WorkoutContext> contextFactory)
            : base(contextFactory) => this.contextFactory = contextFactory;

        public AppUser GetAppUser(string email)
        {
            var dbContext = contextFactory.Invoke();

            return dbContext.AppUsers.FirstOrDefault(appUser => appUser.Email == email);
        }

        public AppUser UpdateRefreshToken(int appUserId, string refreshToken)
        {
            var dbContext = contextFactory.Invoke();
            var entity = dbContext.AppUsers.Find(appUserId);
            entity.RefreshToken = refreshToken;
            dbContext.SaveChanges();

            return entity;
        }
    }
}
