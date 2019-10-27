using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SeriesBusiness = DT.Business.Entities.Series;

namespace DT.Client.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IDataService dataService;
        private readonly ISeriesDataService seriesDataService;

        public SeriesController(ISeriesDataService seriesDataService)
        {
            
            this.seriesDataService = seriesDataService;
            dataService = this.seriesDataService as IDataService;
        }

        [HttpGet]
        public async Task<ActionResult<Series>> Get(int workoutItemId)
        {
            var series = await seriesDataService.GetByWorkoutItemId(workoutItemId);

            return Ok(series);
        }

        [HttpPost]
        public async Task<ActionResult<Series>> Post(Series series)
        {
            var insertedTag = await dataService.AddAsync<Series, SeriesBusiness>(series);

            return Ok(insertedTag);
        }

        [HttpPut]
        public async Task<ActionResult<Series>> Put(Series series)
        {
            var updatedTag = await dataService.UpdateAsync<Series, SeriesBusiness>(series);

            return Ok(updatedTag);
        }

        [HttpDelete]
        public async Task<ActionResult<Series>> Delete(int id)
        {
            var deletedTag = await dataService.RemoveAsync<Series, SeriesBusiness>(id);

            return Ok(deletedTag);
        }
    }
}