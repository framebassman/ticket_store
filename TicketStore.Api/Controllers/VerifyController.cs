using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Data;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<VerifyController> _log;
        private const string _token = "Bearer pkR9vfZ9QdER53mf";

        public VerifyController(ApplicationContext context, ILogger<VerifyController> log)
        {
            _db = context;
            _log = log;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Barcode barcode)
        {
            if (!ModelState.IsValid)
            {
                using (var input = HttpContext.Request.Body)
                {
                    using (var output = new StreamReader(input))
                    {
                        _log.LogWarning("Invalid request data. Request was: {0}", output.ReadToEnd());
                    }
                }
                return new BadRequestObjectResult(new BadRequestAnswer());
            }

            var tickets = _db.Tickets.Where(t => t.Number == barcode.code).ToList();
            if (tickets.Count == 0)
            {
                _log.LogInformation("There is no ticket with this ticket number");
                return new BadRequestObjectResult(new InvalidCodeAnswer());
            }

            var ticket = tickets.First(t => t.Expired == false);
            if (ticket == null)
            {
                _log.LogInformation("Ticket has been already expired");
                return new BadRequestObjectResult(new AlreadyVerifiedAnswer());
            }

            _log.LogDebug("Prepare to updating ticket to expired");
            ticket.Expired = true;
            _db.Tickets.Update(ticket);
            _db.SaveChanges();
            _log.LogDebug("Update ticket to expired");
            return new OkObjectResult(new Answer("OK"));
        }
    }
}
