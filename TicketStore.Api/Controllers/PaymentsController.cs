using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        private EmailService _emailService;
        private IConverter _converter;
        private String _time;

        public PaymentsController(
            ApplicationContext context,
            ILogger<PaymentsController> log,
            IConfiguration config,
            IConverter pdfConverter,
            EmailService emailService
        )
        {
            _db = context;
            _log = log;
            _emailService = emailService;
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
            if (!new Validator(
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
                return new BadRequestObjectResult("Secret is not matching");
            }
            
            _log.LogInformation("Receive Yandex.Money request from {@0}", email);
            var tickets = CombineTickets(label, new Payment { Email = email, Amount = withdraw_amount});
            
            if (tickets.Count == 0)
            {
                return new OkObjectResult("Payment is less than ticket cost");
            }
            var pdf = new Pdf(label, _time, tickets[0].Roubles, tickets, _converter);
            _log.LogInformation("Combined PDF with barcodes");
            _emailService.SendTicket(email, pdf);
            return new OkObjectResult("OK");
        }

        private List<Ticket> CombineTickets(String label, Payment payment)
        {
            _log.LogInformation("Receive payment: {@0}", payment);
            var ticketCost = CalculateTicketCost(label);
            var savedTickets = _db.Tickets.ToList();
            var ticketsToSave = new List<Ticket>();
            int count = CalculateTicketsCount(payment.Amount, ticketCost);
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

        private Int32 CalculateTicketsCount(Decimal amount, Decimal cost)
        {
            return Convert.ToInt32(
                Math.Truncate(
                    Decimal.Divide(amount, cost)
                )
            );
        }

        private Decimal CalculateTicketCost(string label)
        {
            if (label.ToLower().Contains("distemper"))
            {
                _time = "Пятница, 4 октября 2019 года, 19:00";
                _log.LogInformation("Label {@0} contains 'distemper'. Price is 2. Time is {@1}", label, _time);
                return new decimal(2);
            } 
            else if (label.ToLower().Contains("глеб самойлов"))
            {
                _time = "Суббота, 14 сентября 2019 года, 19:00";
                _log.LogInformation("Label {@0} contains 'глеб самойлов'. Price is 3. Time is {@1}", label, _time);
                return new decimal(3);
            }
            _time = "Безвременье";
            _log.LogInformation("Return default price with default time");
            return new decimal(2);
        }
    }
}
