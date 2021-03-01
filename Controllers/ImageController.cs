using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image_box.Models.ImageProcessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Image_box.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IConfiguration _config;
        

        public ImageController(ILogger<ImageController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public string Get()
        {
            return "testing";
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> images)
        {
            // TODO: Create Batch Processor class.
            var compressedFileSizes = new List<int>();
            try
            {
                foreach (var file in images)
                {
                    var fileStorage = new ImageFileSystemStore(
                        _config["ImageProcessor:OutPutFolder"],
                    _config.GetValue<int>("ImageProcessor:MaxFileSize"));

                   compressedFileSizes.Add(await ImageProcessor.CompressAndStore(file, fileStorage));
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(compressedFileSizes);

        }
    }

}