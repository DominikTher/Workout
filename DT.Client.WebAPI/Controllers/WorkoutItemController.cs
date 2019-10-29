using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Mvc;

using WorkoutItemBusiness = DT.Business.Entities.WorkoutItem;

namespace DT.Client.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutItemController : ControllerBase
    {
        private readonly IWorkoutItemDataService workoutItemDataService;
        private readonly IDataService dataService;

        public WorkoutItemController(IWorkoutItemDataService workoutItemDataService)
        {
            this.workoutItemDataService = workoutItemDataService;
            dataService = workoutItemDataService as IDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutItem>>> Get(int workoutId)
        {
            var workoutItems = await workoutItemDataService.GetAsync(workoutId);

            return Ok(workoutItems.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutItem>> Post(WorkoutItem WorkoutItem)
        {
            var insertedWorkoutItem = await dataService.AddAsync<WorkoutItem, WorkoutItemBusiness>(WorkoutItem);

            return Ok(insertedWorkoutItem);
        }

        [HttpPut]
        public async Task<ActionResult<WorkoutItem>> Put(WorkoutItem WorkoutItem)
        {
            var updatedWorkoutItem = await dataService.UpdateAsync<WorkoutItem, WorkoutItemBusiness>(WorkoutItem);

            return Ok(updatedWorkoutItem);
        }

        [HttpDelete]
        public async Task<ActionResult<WorkoutItem>> Delete(int id)
        {
            var deletedWorkoutItem = await dataService.RemoveAsync<WorkoutItem, WorkoutItemBusiness>(id);

            return Ok(deletedWorkoutItem);
        }
    }
}