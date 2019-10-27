using AutoMapper;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeriesBusiness = DT.Business.Entities.Series;

namespace DT.Business.Services
{
    public class SeriesDataService : BaseDataService, ISeriesDataService
    {
        private readonly ISeriesRepository seriesRepository;
        private readonly IMapper mapper;

        public SeriesDataService(ISeriesRepository seriesRepository, IMapper mapper) 
            : base(seriesRepository as IEntityRepository, mapper)
        {
            this.seriesRepository = seriesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Series>> GetByWorkoutItemId(int workoutItemId)
        {
            var seriesBusiness = await seriesRepository.GetByWorkoutItemId(workoutItemId);

            return seriesBusiness.Select(series => mapper.Map<SeriesBusiness, Series>(series));
        }
    }
}
