using System;
using System.IO;

namespace TicketStore.Api.Model.Pdf.Model
{
    public class Layout : TemplateModel
    {
        private Ticket _ticket;
        
        public Layout(Ticket ticket)
        {
            _ticket = ticket;
        }

        protected override string PathToTemplate()
        {
            return Path.Combine("Model", "Pdf", "Templates", "Layout.html");
        }
        
        public override string ToHtml()
        {
            var template = ReadTemplate();
            return template.Replace("%TICKET%", _ticket.ToHtml());
        }
    }
}