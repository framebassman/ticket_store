using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Data;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private ApplicationContext _db;
        private ILogger<PaymentsController> _log;
        private YandexService _yandex;

        public PaymentsController(ApplicationContext context, ILogger<PaymentsController> log, IConfiguration config)
        {
            _db = context;
            _log = log;
            _yandex = new YandexService(config.GetValue<String>("EmailSenderPassword"), _log);
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
                _log.LogInformation("Receive Yandex.Money request from {@0}", email);
                email = "framebassman@gmail.com";
                var tickets = CombineTickets(new Payment { Email = email, Amount = amount});
                _yandex.SendTicket(email);
                return new OkObjectResult("OK");
            }
            else
            {
                return new BadRequestObjectResult("Secret is not matching");
            }
        }

        private List<Ticket> CombineTickets(Payment payment)
        {
            _log.LogInformation("Receive payment: {@0}", payment);
            var saved = _db.Add(payment);
            _db.SaveChanges();

            var tickets = _db.Tickets.ToList();
            var result = new List<Ticket>();
            int count = Convert.ToInt32(payment.Amount) / 250;
            _log.LogInformation($"Combine {count} tickets");
            for (int i = 0; i < count; i++)
            {
                result.Add(new Ticket {
                    CreatedAt = DateTime.Now,
                    PaymentId = payment.Id,
                    Number = new BobJenkinsAlgorithm(tickets.Concat(result).ToList()).Next(),
                    Roubles = payment.Amount,
                    Expired = false
                });
            }
            _db.Tickets.AddRange(result);
            _db.SaveChanges();
            return result;
        }
    }
}
