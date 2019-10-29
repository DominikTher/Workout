using DT.Business.Entities;
using DT.Business.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.DataRepository.Repositories
{
    public class WorkoutItemRepository : BaseRepository, IWorkoutItemRepository
    {
        private readonly Func<WorkoutContext> workoutContextFactory;

        public WorkoutItemRepository(Func<WorkoutContext> workoutContextFactory)
            : base(workoutContextFactory) => this.workoutContextFactory = workoutContextFactory;

        public async Task<IEnumerable<WorkoutItem>> GetAsync(int workoutId)
        {
            using (var workoutContext = workoutContextFactory.Invoke())
            {
                return await workoutContext.WorkoutItems.Where(workoutItem => workoutItem.WorkoutId == workoutId).ToListAsync();
            }
        }
    }
}
