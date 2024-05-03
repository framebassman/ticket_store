using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DinkToPdf.Contracts;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Model.Payment.YandexMoney;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private ApplicationContext _db;
        private ILogger<PaymentsController> _log;
        protected EmailService EmailService;
        protected IConverter PdfConverter;
        protected Converter BarcodeConverter;
        protected HttpClient HttpClient;
        protected IPaymentValidator Validator;

        public PaymentsController(
            ApplicationContext context,
            ILogger<PaymentsController> log,
            IConverter pdfConverter,
            Converter barcodeConverter,
            EmailService emailService,
            IHttpClientFactory clientFactory,
            IPaymentValidator validator
        )
        {
            _db = context;
            _log = log;
            EmailService = emailService;
            PdfConverter = pdfConverter;
            BarcodeConverter = barcodeConverter;
            HttpClient = clientFactory.CreateClient();
            Validator = validator;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(
            [FromForm] String? notification_type,
            [FromForm] String? operation_id,
            [FromForm] Decimal? amount,
            [FromForm] Decimal? withdraw_amount,
            [FromForm] String? currency,
            [FromForm] DateTime? datetime,
            [FromForm] String? email,
            [FromForm] String? sender,
            [FromForm] Boolean? codepro,
            [FromForm] String? label,
            [FromForm] String? sha1_hash
        )
        {
            if (string.IsNullOrEmpty(email))
            {
                _log.LogInformation("Receive Yandex request without email");
                return new OkObjectResult("It's OK for yandex testing");
            }
            email = NormalizeEmail(email);
            _log.LogInformation("Receive Yandex.Money request from {@0} about {@1}", email, label);
            var concert = _db.Events
                .FirstOrDefault(e =>
                    label.Contains(e.Artist)
                );
            if (concert == null)
            {
                return new BadRequestObjectResult("There is no event for merchant");
            }

            var merchant = _db.Merchants.First(m => m.Id == concert.MerchantId);
            if (!Validator.FromYandex(
                    notification_type,
                    operation_id,
                    amount,
                    currency,
                    datetime,
                    sender,
                    codepro,
                    merchant.YandexMoneyAccount,
                    label,
                    sha1_hash
                )
            )
            {
                return new BadRequestObjectResult("Secret is not matching");
            }
            
            var tickets = CombineTickets(concert, new Payment { Email = email, Amount = withdraw_amount.Value});
            
            if (tickets.Count == 0)
            {
                return new OkObjectResult("Payment is less than ticket cost");
            }
            SendTickets(concert, tickets, email);
            return new OkObjectResult("OK");
        }

        [NonAction]
        public virtual void SendTickets(Event concert, List<Ticket> tickets, String email)
        {
            var pdf = new Pdf(concert, tickets, PdfConverter, BarcodeConverter, HttpClient);
            _log.LogInformation("Combined PDF with barcodes");
            EmailService.SendTicket(email, pdf);
        }

        private String NormalizeEmail(String origin)
        {
            return origin.Trim();
        }

        private List<Ticket> CombineTickets(Event concert, Payment payment)
        {
            _log.LogInformation("Receive payment: {@0}", payment);
            var ticketCost = concert.Roubles;
            var savedTickets = _db.Tickets.Where(t => t.Event == concert).ToList();
            var ticketsToSave = new List<Ticket>();
            int count = CalculateTicketsCount(payment.Amount, ticketCost);
            _log.LogInformation($"Combine {count} tickets");
            for (int i = 0; i < count; i++)
            {
                var ticket = new Ticket
                {
                    CreatedAt = DateTime.UtcNow,
                    Number = new Algorithm(savedTickets.Concat(ticketsToSave).ToList()).Next(),
                    Roubles = ticketCost,
                    Expired = false,
                    EventName = concert.Artist,
                    Event = concert
                };
                ticketsToSave.Add(ticket);
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
    }
}
