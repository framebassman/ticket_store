using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Model.Validation;
using TicketStore.Data;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<VerifyController> _log;
        private readonly ITicketFinder _finder;
        private const string _token = "Bearer pkR9vfZ9QdER53mf";

        public VerifyController(ApplicationContext context, ILogger<VerifyController> log, ITicketFinder finder)
        {
            _db = context;
            _log = log;
            _finder = finder;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Barcode barcode)
        {
            _log.LogInformation("Search the following barcode: {@barcode}", barcode);
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

            var ticket = _finder.Find(barcode);
            if (ticket == null)
            {
                _log.LogInformation("There is no ticket found");
                return new BadRequestObjectResult(new InvalidCodeAnswer());
            }

            var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
            if (concert == null)
            {
                _log.LogInformation("Ticket doesn't match any concert");
                return new BadRequestObjectResult(new NoConcertFoundAnswer());
            }

            var labelCalc = new LabelCalculator(concert);

            if (ticket.Expired == true)
            {
                _log.LogInformation("Ticket has already expired");
                return new OkObjectResult(
                    new UsedTicketAnswer(labelCalc.Value())
                );
            }

            _log.LogDebug("Prepare to updating ticket to expired");
            ticket.Expired = true;
            _db.Tickets.Update(ticket);
            _db.SaveChanges();
            _log.LogDebug("Update ticket to expired");
            _log.LogInformation("Ticket is valid");
            return new OkObjectResult(
                new ValidTicketAnswer(labelCalc.Value())
            );
        }
    }
}
