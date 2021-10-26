using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.BusinessLayer.Services;
using SampleWebApi.Shared.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public AuthController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginResponse = await identityService.LoginAsync(request);
            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }

            return BadRequest();
        }

        [HttpGet("me")]
        public IActionResult GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return NoContent();
        }

        [HttpGet("policyme")]
        [Authorize(Policy = "OnlyUSContry")]
        public IActionResult GetUser_2()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return NoContent();
        }

        [HttpGet("18me")]
        [Authorize(Policy = "AtLeast18")]
        [Authorize(Policy = "RequireActiveUser")]
        public IActionResult GetUser_AtLeast18()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return NoContent();
        }

        [HttpGet("activeme")]
        [Authorize(Policy = "RequireActiveUser")]
        public IActionResult GetActiveUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return NoContent();
        }
    }
}
