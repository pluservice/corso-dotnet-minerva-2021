using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.BusinessLayer.Services;

namespace SampleWebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        //[Consumes("multipart/form-data")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Save(IFormFile file)
        {
            await imageService.SaveAsync(file.OpenReadStream(), file.FileName);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string fileName)
        {
            var result = await imageService.ReadAsync(fileName);
            if (result != null)
            {
                var contentType = MimeMapping.MimeUtility.GetMimeMapping(fileName);
                return File(result, contentType);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string fileName)
        {
            await imageService.DeleteAsync(fileName);
            return NoContent();
        }
    }
}
