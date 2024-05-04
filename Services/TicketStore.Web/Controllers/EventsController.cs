using Microsoft.AspNetCore.Mvc;
using TicketStore.Data;
using TicketStore.Web.Model;
using TicketStore.Web.Model.Events;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventsController
    {
        private ILogger<EventsController> _log;
        private ApplicationContext _db;
        private AbstractCustomStuff _dateTime;

        public EventsController(ILogger<EventsController> log, ApplicationContext db, AbstractCustomStuff dateTime)
        {
            _log = log;
            _db = db;
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
