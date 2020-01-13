using System;
using TicketStore.Api.Controllers;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview
{
    public class CountOfSentEmails
    {
        private readonly PaymentsController _controller;
        
        public CountOfSentEmails()
        {
            _controller = new FakePaymentsController(null, null, null, null, null);
        }
        
        [Fact]
        public void ReceivePaymentFor1Ticket_Combine1Ticket_1TicketShouldBeSent()
        {
            // Arrange
            
            // Act
            _controller.Post(
                false,
                null,
                null,
                0,
                0,
                null,
                DateTime.Now, 
                false,
                null,
                null,
                null,
                false,
                null
            );

            // Assert
        }
    }
}