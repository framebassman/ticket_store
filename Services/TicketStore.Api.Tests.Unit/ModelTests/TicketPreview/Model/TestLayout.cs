using System;
using TicketStore.Api.Model.Pdf.Model;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model
{
    public class TestLayout : Layout
    {
        private readonly String _template;

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
