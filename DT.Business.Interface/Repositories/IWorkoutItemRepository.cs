using DT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Interface.Repositories
{
    public interface IWorkoutItemRepository
    {
        Task<IEnumerable<WorkoutItem>> GetAsync(int workoutId);
    }
}
