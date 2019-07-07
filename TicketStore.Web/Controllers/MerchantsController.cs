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
        private readonly ILogger<MerchantsController> _log;
        private readonly ApplicationContext _db;

        public MerchantsController(ILogger<MerchantsController> log, ApplicationContext db)
        {
            _log = log;
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _db.Merchants.ToList();
            _log.LogInformation("Return hardcoded merchants: {@Result}", result);
            return new OkObjectResult(result);
        }

    }
}
