using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Poster;
using TicketStore.Api.Model.Validation;
using TicketStore.Data;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPosterController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<VerifyController> _log;

        public UploadPosterController(ApplicationContext context, ILogger<VerifyController> log, ITicketFinder finder)
        {
            _db = context;
            _log = log;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Poster poster)
        {
            var concert = _db.Events.FirstOrDefault(e => e.Id == poster.eventId);
            return new OkObjectResult(@"{concert.poster_url}{poster.imageUrl}");
        }
    }
}
