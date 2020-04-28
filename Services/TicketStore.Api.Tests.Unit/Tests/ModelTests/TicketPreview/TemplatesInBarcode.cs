using TicketStore.Api.Model.PdfDocument.Model;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview
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
