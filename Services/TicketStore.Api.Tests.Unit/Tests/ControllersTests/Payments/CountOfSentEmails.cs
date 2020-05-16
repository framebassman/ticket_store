using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Api.Model;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Payments
{
    public class CountOfSentEmails : PaymentsControllerBaseTest
    {
        public CountOfSentEmails() : base("count_of_emails")
        {
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
            var result = Controller.Post(null,
                null,
                0,
                concert.Roubles,
                null,
                concertTime - TimeSpan.FromHours(1),
                email,
                null,
                false,
                new LabelCalculator(Logger, concert).Value(),
                ""
            );

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Single((IEnumerable) EmailService.PdfList(email));
        }
    }
}