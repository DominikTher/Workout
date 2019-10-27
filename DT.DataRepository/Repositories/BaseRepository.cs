using DT.Business.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.DataRepository.Repositories
{
    public class BaseRepository : IEntityRepository
    {
        private readonly Func<WorkoutContext> contextFactory;

        public BaseRepository(Func<WorkoutContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            using (var workoutContext = contextFactory.Invoke())
            {
                var dbEntity = workoutContext.Add(entity);
                await workoutContext.SaveChangesAsync();

                return dbEntity.Entity;
            }
        }

        public async Task<IEnumerable<T>> GetAsync<T>() where T : class
        {
            using (var workoutContext = contextFactory.Invoke())
            {
                return await workoutContext.Set<T>().ToListAsync();
            }
        }

        public async Task<T> RemoveAsync<T>(int id) where T : class
        {
            var entityInstance = Activator.CreateInstance<T>();
            var type = typeof(T);
            var prop = type.GetProperty("Id");
            prop.SetValue(entityInstance, id, null);

            using (var workoutContext = contextFactory.Invoke())
            {
                var dbEntity = workoutContext.Remove(entityInstance);
                await workoutContext.SaveChangesAsync();

                return dbEntity.Entity;
            }
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            using (var workoutContext = contextFactory.Invoke())
            {
                var dbEntity = workoutContext.Update(entity);
                await workoutContext.SaveChangesAsync();

                return dbEntity.Entity;
            }
        }
    }
}
