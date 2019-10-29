using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ExerciseTagBusiness = DT.Business.Entities.ExerciseTag;

namespace DT.Client.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTagController : ControllerBase
    {
        private readonly IDataService dataService;

        public ExerciseTagController(IDataService dataService) => this.dataService = dataService;

        [HttpPost]
        public async Task<ActionResult<ExerciseTag>> Post(ExerciseTag exerciseTag)
        {
            var insertedExerciseTag = await dataService.AddAsync<ExerciseTag, ExerciseTagBusiness>(exerciseTag);

            return Ok(insertedExerciseTag);
        }

        [HttpPut]
        public async Task<ActionResult<ExerciseTag>> Put(ExerciseTag exerciseTag)
        {
            var updatedExerciseTag = await dataService.UpdateAsync<ExerciseTag, ExerciseTagBusiness>(exerciseTag);

            return Ok(updatedExerciseTag);
        }

        [HttpDelete]
        public async Task<ActionResult<Workout>> Delete(ExerciseTag exerciseTag)
        {
            throw new NotImplementedException();
        }
    }
}