using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Poster;
using TicketStore.Data;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPosterController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<VerifyController> _log;
        private PosterUpdater _updater;

        public UploadPosterController(ApplicationContext context, ILogger<VerifyController> log, PosterUpdater updater)
        {
            _db = context;
            _log = log;
            _updater = updater;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Poster poster)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);

            var imageNameUrl = await _updater.Update(poster);

            return new OkObjectResult(imageNameUrl);
        }
    }
}
