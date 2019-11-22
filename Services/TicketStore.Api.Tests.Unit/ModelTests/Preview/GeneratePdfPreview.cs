using TicketStore.Api.Tests.Unit.ModelTests.Preview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests.Preview
{
    public class GeneratePdfPreview
    {
        [Fact]
        public void Some()
        {
            // Arrange
            var template = "Picture: %PICTURE% Number: %NUMBER%";
            var barcode = new TestBarcode(template, "111", new UnitTestConverter());

            // Act
            var html = barcode.ToHtml();

            // Assert
            Assert.Equal("Picture: base64 Number: 111", html);
        }
    }
}