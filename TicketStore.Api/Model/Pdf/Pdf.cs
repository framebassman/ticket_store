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
                        HtmlContent = $@"
    <div style=""text-align: center"">
      <div style=""font-weight: 500;font-size: 20px;"">Электронные билеты</div>
      <div style=""border: 4px solid yellow;max-width: 500px; margin-left: auto; margin-right: auto"">
        <div style=""text-align: left; margin: 4px"">The Cellophane Heads - X лет</div>
        <div style=""border: 1px solid black; margin: 4px"">
          <div style=""display: flex; justify-content: space-between;"">
            <div style=""font-size: 14px; margin: 4px; text-align: left"">Суббота, 20 апреля 2019 года, 20:00</div>
            <div style=""font-size: 14px; margin: 4px; text-align: right"">Стоимость: 200 ₽</div>
          </div>
          {images}
        </div>
      </div>
      <div style=""max-width: 600px; margin-left: auto; margin-right: auto;"">
        <div style=""margin: 16px; font-weight: 500;"">РАСПЕЧАТАЙТЕ этот бланк и предъявите при проходе на мероприятие</div>
        <div style=""font-size: 7px"">
          <div>Внимание: Штрих-код, указанный на ЭБ, действителен только для однократного прохода на мероприятие. Не допускайте
          перепечатки и копирования Вашего ЭБ третьими лицами, так как они могут воспользоваться им раньше Вас! Настоящий
          документ является такой же ценностью, как и наличные деньги. Хранение ЭБ, недопущение его копирования и/или иного
          воспроизведение является обязанностью Клиента. Организатор не несет ответственности за сохранность Ваших ЭБ.
          Организатор не несет ответственности за билеты, приобретенные у третьих лиц, в том числе приобретенные «с рук»
          </div>
          <div style=""margin: 16px;"">
            <div style=""font-weight: 600"">Порядок прохождения контроля с ЭБ</div>
            <div>
              При проходе на мероприятие Клиент обязан иметь при себе распечатанный бланк ЭБ на каждое место. При посещении мероприятия
              все Клиенты с ЭБ проходят каждый по своему билету.
            </div>
          </div>
          <div style=""margin: 16px;"">
            <div style=""font-weight: 600"">Меры безопасности</div>
            <div>
              Клиент обязан бережно относится к ЭБ и не допускать его копирования и деформирования. Учитывая, что с момента получения ЭБ,
              хранение ЭБ, недопущение его копирования и/или иного воспроизведения является ОБЯЗАННОСТЬЮ Клиента, организатор не несет
              ответственности за наличие двойных билетов. В случае возникновения сомнений в подлинности ЭБ, выявления случаев «двойных
              билетов», сотрудники зрелищного учреждения ВПРАВЕ ОТКАЗАТЬ в посещении мероприятии ВСЕМ лицам, предъявившим спорный ЭБ.
            </div>
          </div>
        </div>
      </div>
    </div>
                        ",
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
