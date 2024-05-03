using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend_localinezationBackend.Services;
using System.Threading.Tasks;

namespace Backend_localinezationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly MediaService _mediaService;

        public MediaController(ILogger<MediaController> logger, MediaService mediaService)
        {
            _logger = logger;
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedia()
        {
            try
            {
                var media = await _mediaService.GetAllMediaAsync();
                return Ok(media);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting media");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
