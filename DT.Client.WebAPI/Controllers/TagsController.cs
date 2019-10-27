using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.AspNetCore.Mvc;

using TagBusiness = DT.Business.Entities.Tag;

namespace DT.Client.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IDataService dataService;

        public TagsController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> Get()
        {
            var tags = await dataService.GetAsnyc<Tag, TagBusiness>();

            return Ok(tags.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Post(Tag workout)
        {
            var insertedTag = await dataService.AddAsync<Tag, TagBusiness>(workout);

            return Ok(insertedTag);
        }

        [HttpPut]
        public async Task<ActionResult<Tag>> Put(Tag workout)
        {
            var updatedTag = await dataService.UpdateAsync<Tag, TagBusiness>(workout);

            return Ok(updatedTag);
        }

        [HttpDelete]
        public async Task<ActionResult<Tag>> Delete(int id)
        {
            var deletedTag = await dataService.RemoveAsync<Tag, TagBusiness>(id);

            return Ok(deletedTag);
        }
    }
}