using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExcersiseBusiness = DT.Business.Entities.Exercise;

namespace DT.Client.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IDataService dataService;

        public ExerciseController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> Get()
        {
            var exercises = await dataService.GetAsnyc<Exercise, ExcersiseBusiness>();

            return Ok(exercises.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> Post(Exercise workout)
        {
            var insertedExercise = await dataService.AddAsync<Exercise, ExcersiseBusiness>(workout);

            return Ok(insertedExercise);
        }

        [HttpPut]
        public async Task<ActionResult<Exercise>> Put(Exercise workout)
        {
            var updatedExercise = await dataService.UpdateAsync<Exercise, ExcersiseBusiness>(workout);

            return Ok(updatedExercise);
        }

        [HttpDelete]
        public async Task<ActionResult<Exercise>> Delete(int id)
        {
            var deletedExercise = await dataService.RemoveAsync<Exercise, ExcersiseBusiness>(id);

            return Ok(deletedExercise);
        }
    }
}