using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Data;
using TicketStore.Api.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private ApplicationContext _db;

        public PaymentsController(ApplicationContext context)
        {
            _db = context;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(
            [FromForm] Boolean test_notification,
            [FromForm] String notification_type,
            [FromForm] String operation_id,
            [FromForm] Decimal amount,
            [FromForm] Decimal withdraw_amount,
            [FromForm] String currency,
            [FromForm] DateTime datetime,
            [FromForm] Boolean unaccepted,
            [FromForm] String email,
            [FromForm] String lastname,
            [FromForm] String sender,
            [FromForm] Boolean codepro,
            [FromForm] String label
        )
        {
            if (new Validator(
                    notification_type,
                    operation_id,
                    amount,
                    currency,
                    datetime,
                    sender,
                    codepro,
                    "",
                    label
                ).FromYandex()
            )
            {
                var tickets = CombineTickets(new Payment { Email = email, Amount = amount});
                return new OkObjectResult("OK");
            }
            else
            {
                return new BadRequestObjectResult("Secret is not matching");
            }
        }

        private List<Ticket> CombineTickets(Payment payment)
        {
            var saved = _db.Add(payment);
            _db.SaveChanges();

            var tickets = _db.Tickets.ToList();
            var result = new List<Ticket>();
            int count = Convert.ToInt32(payment.Amount) / 250;
            for (int i = 0; i < count; i++)
            {
                result.Add(new Ticket {
                    CreatedAt = DateTime.Now,
                    PaymentId = payment.Id,
                    Number = new BobJenkinsAlgorithm(tickets.Concat(result).ToList()).Next(),
                });
            }
            _db.Tickets.AddRange(result);
            _db.SaveChanges();
            return result;
        }
    }
}