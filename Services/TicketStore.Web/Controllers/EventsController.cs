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
        private IDateTimeProvider _customStuff;

        public EventsController(ILogger<EventsController> log, ApplicationContext db, IDateTimeProvider customStuff)
        {
            _log = log;
            _db = db;
            _customStuff = customStuff;
        }

        [HttpGet]
        public IActionResult Get(Int32 merchantId)
        {
            if (merchantId == 0)
            {
                _log.LogWarning("Request without merchantId parameter");
                return new BadRequestObjectResult("Request should contains merchantId parameter");
            }

            var result = new EventsFinder(_db, merchantId, _customStuff).Find();
            _log.LogInformation("Return events: {@result} for merchantId: {@merchantId}", result, merchantId);
            return new OkObjectResult(result);
        }
    }
}
