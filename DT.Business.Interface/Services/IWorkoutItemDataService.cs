using DT.Client.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Interface.Services
{
    public interface IWorkoutItemDataService
    {
        Task<IEnumerable<WorkoutItem>> GetAsync(int workoutId);
    }
}
