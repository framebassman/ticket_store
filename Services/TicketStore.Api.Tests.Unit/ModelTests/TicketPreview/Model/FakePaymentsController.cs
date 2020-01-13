using System.Net.Http;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Controllers;
using TicketStore.Api.Model.Email;
using TicketStore.Data;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model
{
    public class FakePaymentsController : PaymentsController
    {
        public FakePaymentsController(
            ApplicationContext context,
            ILogger<PaymentsController> log,
            IConverter pdfConverter,
            EmailService emailService,
            IHttpClientFactory clientFactory
        ) : base(context, log, pdfConverter, emailService, clientFactory)
        {
        }
    }
}