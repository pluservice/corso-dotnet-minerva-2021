using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
using SampleWebApi.Services;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(Person person)
        {
            await peopleService.SaveAsync(person);
            return NoContent();
        }
    }
}
