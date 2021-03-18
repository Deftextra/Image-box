using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Image_box.Models.ImageProcessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Image_box.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ImageFileSystemStore _fileSystemStore;
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _fileSystemStore = new ImageFileSystemStore(
                _config["ImageProcessor:OutPutFolder"],
                _config.GetValue<int>("ImageProcessor:MaxFileSize"));
        }

        [HttpGet]
        public IActionResult Get(int id, string token)
        {
            return Ok();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> Post(List<IFormFile> images)
        {
            var compressedFileSizes = new List<int>();
            try
            {
                foreach (var file in images)
                    compressedFileSizes.Add(await ImageProcessor.CompressAndStore(file, _fileSystemStore));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(compressedFileSizes);
        }

        // HTTPS connections are reused and optimized so this is probably more optimal
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile image)
        {
            try
            {
                return Ok(await ImageProcessor.CompressAndStore(image, _fileSystemStore));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}