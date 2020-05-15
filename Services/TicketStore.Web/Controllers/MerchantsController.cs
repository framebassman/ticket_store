using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Data;

namespace TicketStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private ILogger<MerchantsController> _log;
        private ApplicationContext _db;

        public MerchantsController(ILogger<MerchantsController> log, ApplicationContext db)
        {
            _log = log;
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _db.Merchants.ToList();
            _log.LogInformation("Return merchants: {@Result} from db", result);
            return new OkObjectResult(result);
        }

    }
}
