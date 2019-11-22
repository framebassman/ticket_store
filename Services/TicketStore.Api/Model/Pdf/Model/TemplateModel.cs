using System;
using System.IO;

namespace TicketStore.Api.Model.Pdf.Model
{
    public abstract class TemplateModel
    {
        public abstract String ToHtml();

        protected abstract String PathToTemplate();

        protected String ReadTemplate()
        {
            using (var reader = new StreamReader(PathToTemplate()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}