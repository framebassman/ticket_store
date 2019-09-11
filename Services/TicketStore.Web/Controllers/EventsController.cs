using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Data;
using TicketStore.Web.Model;
using TicketStore.Web.Model.Events;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _log;
        private readonly ApplicationContext _db;
        private readonly Random _random;
        private readonly IDateTimeProvider _dateTime;

        public EventsController(ILogger<EventsController> log, ApplicationContext db, IDateTimeProvider dateTime)
        {
            _log = log;
            _db = db;
            _random = new Random();
            _dateTime = dateTime;
        }

        [HttpGet]
        public IActionResult Get(Int32 merchantId)
        {
            if (merchantId == 0)
            {
                _log.LogWarning("Request without merchantId parameter");
                return new BadRequestObjectResult("Request should contains merchantId parameter");
            }

            var result = new EventsFinder(_db, merchantId, _dateTime).Find();
            _log.LogInformation("Return events: {@result} for merchantId: {@merchantId}", result, merchantId);
            return new OkObjectResult(result);
        }
    }
}
