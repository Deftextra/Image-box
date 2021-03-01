using System.Collections.Generic;
using System.Threading.Tasks;
using Image_box.Models.ImageProcessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Image_box.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "testing";
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> images)
        {
            foreach (var file in images)
            {
                var fileStorage = new ImageFileSystemStore("./");

                await ImageProcessor.CompressAndStore(file, fileStorage);

            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }

}