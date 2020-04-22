using TicketStore.Api.Model.Pdf.Model;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Api.Tests.Unit.Stubs;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview
{
    public class TemplatesInBarcode
    {
        protected readonly Barcode Barcode;

        public TemplatesInBarcode()
        {
            var template = "Picture: %PICTURE% Number: %NUMBER%";
            var ticketNumber = "111";
            Converter converter = new DummyBarcodeConverter();
            Barcode = new TestBarcode(template, ticketNumber, converter);
        }
        
        [Fact]
        public virtual void RenderHtml_ChangePlaceholdersInTemplate()
        {
            // Act
            var html = Barcode.ToHtml();

            // Assert
            Assert.Equal("Picture: base64 Number: 111", html);
        }
    }
}
