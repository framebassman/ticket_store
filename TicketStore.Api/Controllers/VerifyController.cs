using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Data;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Http;
using TicketStore.Api.Model.Validator;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private ApplicationContext _db;

        public VerifyController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Barcode barcode)
        {
            if (!IsAuthorized(Request))
            {
                return new UnauthorizedObjectResult(new UnauthorizedAnswer());
            }
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestAnswer());
            }

            var tickets = _db.Tickets.Where(t => t.Number == barcode.code).ToList();
            if (tickets.Count == 0)
            {
                return new BadRequestObjectResult(new InvalidCodeAnswer());
            }

            var ticket = tickets.First(t => t.Expired == false);
            if (ticket == null)
            {
                return new BadRequestObjectResult(new AlreadyVerifiedAnswer());
            }

            ticket.Expired = true;
            _db.Tickets.Update(ticket);
            _db.SaveChanges();
            return new OkObjectResult(new Answer("OK"));
        }

        private Boolean IsAuthorized(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
            {
                return false;
            }
            return authHeader == "Bearer pkR9vfZ9QdER53mf";
        }
    }
}
