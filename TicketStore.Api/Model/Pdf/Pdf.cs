using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using DinkToPdf;
using DinkToPdf.Contracts;
using NetBarcode;

namespace TicketStore.Api.Model.Pdf
{
    public class Pdf
    {
        private List<Ticket> _tickets;
        private IConverter _converter;
        public Pdf(List<Ticket> tickets, IConverter converter)
        {
            _tickets = tickets;
            _converter = converter;
        }

        public byte[] toBytes()
        {
            var temp = template();
            return _converter.Convert(temp);
        }

        private HtmlToPdfDocument template()
        {
            var images = barcodes();
            return new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A6,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = $"<div><div>Билеты</div>{images}</div>",
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
        }

        private String barcodes()
        {
            var sb = new StringBuilder();
            foreach (var ticket in _tickets)
            {
                sb.Append($"<img src='data:image/png;base64, {barcode(ticket)}'/>")
                    .Append("<br/>")
                    .Append("<br/>");
            }
            return sb.ToString();
        }

        private String barcode(Ticket ticket)
        {
            var barcode = new Barcode(ticket.Number, NetBarcode.Type.Code128, true);
            return barcode.GetBase64Image();
        }
    }
}
