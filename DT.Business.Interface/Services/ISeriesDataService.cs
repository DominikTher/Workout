using DT.Client.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Interface.Services
{
    public interface ISeriesDataService
    {
        Task<IEnumerable<Series>> GetByWorkoutItemId(int workoutItemId);
    }
}
