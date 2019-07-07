using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Data;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _log;
        private readonly ApplicationContext _db;
        private readonly Random _random;

        public EventsController(ILogger<EventsController> log, ApplicationContext db)
        {
            _log = log;
            _db = db;
            _random = new Random();
        }

        [HttpGet]
        public IActionResult Get(Int32 merchantId)
        {
            if (merchantId == 0)
            {
                _log.LogWarning("Request without merchantId parameter");
                return new BadRequestObjectResult("Request should contains merchantId parameter");
            }

            var result = _db.Events.ToList();
            _log.LogInformation("Return hardcoded events: {@result}", result);
            return new OkObjectResult(result);
        }
    }
}
