using Microsoft.AspNetCore.Mvc;
using SampleWebApi.BusinessLayer.Services;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IPeopleService peopleService;

        public AddressesController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        //[HttpGet("getbypersonid/{id:int}")]
        //public IActionResult GetAddressesByPersonId(int id)
        //{
        //    return NoContent();
        //}
    }
}
