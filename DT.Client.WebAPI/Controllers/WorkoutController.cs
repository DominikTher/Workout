using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WorkoutBusiness = DT.Business.Entities.Workout;

namespace DT.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IDataService dataService;

        public WorkoutController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> Get()
        {
            var workouts = await dataService.GetAsnyc<Workout, WorkoutBusiness>();

            return Ok(workouts.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> Post(Workout workout)
        {
            var insertedWorkout = await dataService.AddAsync<Workout, WorkoutBusiness>(workout);

            return Ok(insertedWorkout);
        }

        [HttpPut]
        public async Task<ActionResult<Workout>> Put(Workout workout)
        {
            var updatedWorkout = await dataService.UpdateAsync<Workout, WorkoutBusiness>(workout);

            return Ok(updatedWorkout);
        }

        [HttpDelete]
        public async Task<ActionResult<Workout>> Delete(int id)
        {
            var deletedWorkout = await dataService.RemoveAsync<Workout, WorkoutBusiness>(id);

            return Ok(deletedWorkout);
        }
    }
}