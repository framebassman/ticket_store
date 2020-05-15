using System;
using TicketStore.Api.Model.PdfDocument.Model;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model
{
    public class TestLayout : Layout
    {
        private String _template;

        public TestLayout(String template, Ticket ticket) : base(ticket)
        {
            _template = template;
        }

        protected override string ReadTemplate()
        {
            return _template;
        }
    }
}
