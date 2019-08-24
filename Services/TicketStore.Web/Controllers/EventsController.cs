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

            var resultTemp = _db.Events
                .Where(e => e.MerchantId == merchantId)
                .ToList();
            
            var result = _db.Events
                .Where(e => e.MerchantId == merchantId)
                .OrderBy(e => e.Time)
                .ToList();
            _log.LogInformation("Return events: {@result} for merchantId: {@merchantId}", result, merchantId);
            return new OkObjectResult(result);
        }
    }
}
