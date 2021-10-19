using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Services;

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
    }
}
