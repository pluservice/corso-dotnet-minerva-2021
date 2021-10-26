using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebApi.BusinessLayer.Services;
using SampleWebApi.Shared.Models;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService peopleService;
        private readonly ILogger<PeopleController> logger;

        public PeopleController(IPeopleService peopleService, ILogger<PeopleController> logger)
        {
            this.peopleService = peopleService;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPeopleList()
        {
            logger.LogDebug("Inizio recupero lista persone...");

            var people = await peopleService.GetListAsync();

            logger.LogInformation("Persone recuperate");

            return Ok(people);
        }

        [HttpGet("{id:int}", Name = nameof(GetPersonById))]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await peopleService.GetAsync(id);
            if (person != null)
            {
                return Ok(person);
            }

            return NotFound();
        }

        //[HttpGet("{id:int}/addresses")]
        //public IActionResult GetAddressesByPersonId(int id)
        //{
        //    return NoContent();
        //}

        //[HttpGet("{personId:int}/addresses/{addressId:int}")]
        //public IActionResult GetAddressByPersonId(int personId, int addressId)
        //{
        //    return NoContent();
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public IActionResult Save(Person person)
        //{
        //    return NoContent();
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        public async Task<IActionResult> Save(SavePersonRequest person)
        {
            var result = await peopleService.SaveAsync(person);

            //var url = Url.Action(nameof(GetPersonById), new { id = 976 });
            //return Created(url, person);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await peopleService.DeleteAsync(id);
            return NoContent();
        }

        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Update(int id, Person person)
        //{
        //    await peopleService.SaveAsync(person);
        //    return NoContent();
        //}

        //[HttpPost("{id:int}/addresses")]
        //public IActionResult SaveAddress(int id)
        //{
        //    return NoContent();
        //}

        //[HttpPut("{personId:int}/addresses/{addressId:int}")]
        //public IActionResult UpdateAddressByPersonId(int personId, int addressId)
        //{
        //    return NoContent();
        //}
    }
}
