using System.IO;

namespace TicketStore.Api.Model.PdfDocument.Model
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
            return Path.Combine("Model", "PdfDocument", "Templates", "Layout.html");
        }
        
        public override string ToHtml()
        {
            var template = ReadTemplate();
            return template.Replace("%TICKET%", _ticket.ToHtml());
        }
    }
}