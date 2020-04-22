using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Api.Controllers;
using TicketStore.Api.Model;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public class CountOfSentEmails : ControllersBaseTest<PaymentsController>
    {
        private readonly PaymentsController _controller;
        private readonly DummyEmailService _emailService;
        
        public CountOfSentEmails() : base("count_of_sent")
        {
            _emailService = new DummyEmailService();
            _controller = new FakePaymentsController(
                Db,
                Logger,
                new DummyConverter(),
                _emailService,
                new DummyHttpClientFactory()
            );
        }
        
        [Fact]
        public void ReceivePaymentFor1Ticket_Combine1Ticket_1TicketShouldBeSent()
        {
            // Arrange
            DateTime concertTime = DateTime.Now;
            SeedTestData(concertTime);
            Event concert = Db.Events.First();
            String email = "test@test.test";
            
            // Act
            var result = _controller.Post(
                false,
                null,
                null,
                0,
                concert.Roubles,
                null,
                concertTime - TimeSpan.FromHours(1), 
                false,
                email,
                null,
                null,
                false,
                new LabelCalculator(Logger, concert).Value()
            );

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Single(_emailService.PdfList(email));
        }
    }
}