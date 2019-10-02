using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Model.Poster;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly ILogger<VerifyController> _log;
        private readonly IPosterUpdater _updater;

        public UploadsController(ILogger<VerifyController> log, IPosterUpdater updater)
        {
            _log = log;
            _updater = updater;
        }

        [HttpPost("poster")]
        public async Task<IActionResult> UpdatePoster([FromBody] Poster poster)
        {
            try
            {
                _log.LogInformation($"Update event ID: {poster.eventId}, Image URL {poster.imageUrl}");
                var imageUrl = await _updater.Update(poster);
                return new OkObjectResult(new SuccessUploadPosterAnswer(imageUrl));
            } 
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new BadRequestObjectResult(new FailedUploadPosterAnswer());
            }
        }
    }
}
