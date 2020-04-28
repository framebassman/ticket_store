using TicketStore.Api.Model.PdfDocument.Model;
using TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview
{
    public class TemplatesInLayout : TemplatesInTicket
    {
        private readonly Layout _layout;

        public TemplatesInLayout()
        {
            _layout = new TestLayout("Ticket: \n%TICKET%\n", Ticket);
        }
        
        [Fact]
        public override void RenderHtml_ChangePlaceholdersInTemplate()
        {
            // Act
            var html = _layout.ToHtml();

            // Assert
            Assert.Equal(html, $"Ticket: \n{Ticket.ToHtml()}\n");
        }
    }
}
