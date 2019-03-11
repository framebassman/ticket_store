using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DinkToPdf.Contracts;
using TicketStore.Api.Data;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Pdf;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private ApplicationContext _db;
        private ILogger<PaymentsController> _log;
        private YandexService _yandex;
        private IConverter _converter;

        public PaymentsController(ApplicationContext context, ILogger<PaymentsController> log, IConfiguration config, IConverter pdfConverter)
        {
            _db = context;
            _log = log;
            _yandex = new YandexService(config.GetValue<String>("EmailSenderPassword"), _log);
            _converter = pdfConverter;
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
            if (string.IsNullOrEmpty(email))
            {
                _log.LogInformation("Receive Yandex request without email");
                return new OkObjectResult("It's OK for yandex testing");
            }
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
                var tickets = CombineTickets(new Payment { Email = email, Amount = amount});
                var pdf = new Pdf(tickets, _converter);
                _log.LogInformation("Combined PDF with barcodes");
                _yandex.SendTicket(email, pdf.toBytes());
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
            var ticketCost = 200;
            var savedTickets = _db.Tickets.ToList();
            var ticketsToSave = new List<Ticket>();
            int count = Convert.ToInt32(payment.Amount) / ticketCost;
            _log.LogInformation($"Combine {count} tickets");
            for (int i = 0; i < count; i++)
            {
                ticketsToSave.Add(new Ticket {
                    CreatedAt = DateTime.Now,
                    Number = new Algorithm(savedTickets.Concat(ticketsToSave).ToList()).Next(),
                    Roubles = ticketCost,
                    Expired = false
                });
            }
            payment.Tickets = ticketsToSave;
            _db.Payments.Add(payment);
            _db.SaveChanges();
            return ticketsToSave;
        }
    }
}
