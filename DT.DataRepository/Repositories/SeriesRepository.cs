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
    public class SeriesRepository : BaseRepository, ISeriesRepository
    {
        private readonly Func<WorkoutContext> seriesContextFactory;

        public SeriesRepository(Func<WorkoutContext> seriesContextFactory)
            : base(seriesContextFactory)
        {
            this.seriesContextFactory = seriesContextFactory;
        }

        public async Task<IEnumerable<Series>> GetByWorkoutItemId(int workoutItemId)
        {
            using var dbContext = seriesContextFactory.Invoke();

            return await dbContext.Series.Where(series => series.WorkoutItemId == workoutItemId).ToListAsync();
        }
    }
}
