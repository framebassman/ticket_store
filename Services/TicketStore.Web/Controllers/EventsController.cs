using System;
using System.Collections.Generic;
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
        private ILogger<EventsController> _log;
        private ApplicationContext _db;
        private IDateTimeProvider _dateTime;

        public EventsController(ILogger<EventsController> log, ApplicationContext db, IDateTimeProvider dateTime)
        {
            _log = log;
            _db = db;
            _dateTime = dateTime;
        }

        [HttpGet]
        public IActionResult Get(Int32 merchantId, Int32 page, Int32 size)
        {
            if (merchantId == 0)
            {
                _log.LogWarning("Request without merchantId parameter");
                return new BadRequestObjectResult("Request should contains merchantId parameter");
            }
            
            if (page < 0 || size < 0)
            {
                _log.LogWarning("Page or size cannot be less than 0");
                return new BadRequestObjectResult("Page or size parameter should be more than 0");
            }

            var result = new EventsFinder(_db, merchantId, _dateTime).Find(page, size);
            _log.LogInformation("Return events: {@result} for merchantId: {@merchantId}", result, merchantId);
            return new OkObjectResult(result);
        }
    }
}
