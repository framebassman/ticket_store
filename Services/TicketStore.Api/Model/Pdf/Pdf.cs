using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using DinkToPdf;
using DinkToPdf.Contracts;
using NetBarcode;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Pdf
{
    public class Pdf
    {
        private String _eventName;
        private DateTime _originTime;
        private Decimal _price;
        private List<Ticket> _tickets;
        private IConverter _converter;
        private TimeZoneInfo _mskTimezone;
        public Pdf(Event concert, List<Ticket> tickets, IConverter converter)
        {
            _mskTimezone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            _eventName = concert.Artist;
            _originTime = concert.Time;
            _price = concert.Roubles;
            _tickets = tickets;
            _converter = converter;
        }

        public byte[] ToBytes()
        {
            var temp = Template();
            return _converter.Convert(temp);
        }

        // TODO: Remove this, Ticket should returns Time
        public String Time()
        {
            return FormatTime(_originTime);
        }

        private String FormatTime(DateTime origin)
        {
            var originTime = DateTime.SpecifyKind(origin, DateTimeKind.Utc);
            var withTimezone = TimeZoneInfo.ConvertTimeFromUtc(originTime, _mskTimezone);
            var allLowerCase = withTimezone.ToString("f", CultureInfo.CreateSpecificCulture("ru-RU"));
            var firstLetterIsCapital = new StringBuilder()
                .Append(allLowerCase[0].ToString().ToUpper())
                .Append(allLowerCase.Substring(1, allLowerCase.Length - 1))
                .ToString();
            return firstLetterIsCapital;
        }

        private HtmlToPdfDocument Template()
        {
            var images = Barcodes();
            return new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = $@"
    <div style=""text-align: center"">
      <div style=""font-weight: 500;font-size: 20px;"">Электронные билеты</div>
      {images}
      <div style=""max-width: 600px; margin-left: auto; margin-right: auto;"">
        <div style=""margin: 16px; font-weight: 500;"">РАСПЕЧАТАЙТЕ этот бланк и предъявите при проходе на мероприятие</div>
        <div style=""margin: 16px; font-weight: 500;"">Билеты возврату и обмену не подлежат</div>
        <div style=""font-size: 10px"">
          <div>Внимание: Штрих-код, указанный на ЭБ, действителен только для однократного прохода на мероприятие. Не допускайте
          перепечатки и копирования Вашего ЭБ третьими лицами, так как они могут воспользоваться им раньше Вас! Настоящий
          документ является такой же ценностью, как и наличные деньги. Хранение ЭБ, недопущение его копирования и/или иного
          воспроизведение является обязанностью Клиента. Организатор не несет ответственности за сохранность Ваших ЭБ.
          Организатор не несет ответственности за билеты, приобретенные у третьих лиц, в том числе приобретенные «с рук»
          </div>
          <div style=""margin: 16px;"">
            <div style=""font-weight: 600"">Правила клуба</div>
            <div>
                Наличие билета на концерт дает право находиться в клубе во время концерта, указанного в билете без посадочного места.
                После окончания концерта администрация имеет право попросить посетителей покинуть зал. 
                В клубе может быть произведен досмотр, с целью обеспечения безопасности его посетителей.
                Посетители не должны оказывать сопротивления досматривающим, в противном случае администрация может отказать 
                клиенту в праве на повторное посещение. Администрация не несет ответственность за билеты,
                приобретённые в неустановленных местах. Клуб не несет ответственности за сохранность личных вещей, транспортных 
                средств и другого имущества, оставленного на территории клуба. Посетителю придется покинуть клуб
                в случае проявления агрессии по отношению к другим гостям и/или к персоналу.
            </div>
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

        private String Barcodes()
        {
            var sb = new StringBuilder();
            foreach (var ticket in _tickets)
            {
                var template = $@"
                  <div style=""border: 4px solid yellow;max-width: 500px; margin-left: auto; margin-right: auto"">
                    <div style=""text-align: left; margin: 4px"">{_eventName}</div>
                    <div style=""border: 1px solid black; margin: 4px"">
                      <div style=""display: flex; justify-content: space-between;"">
                        <div style=""font-size: 14px; margin: 4px; text-align: left"">{FormatTime(_originTime)}</div>
                        <div style=""font-size: 14px; margin: 4px; text-align: right"">Стоимость: {_price} ₽</div>
                      </div>
                      <img src='https://barcode.tec-it.com/barcode.ashx?data={ticket.Number}&code=&multiplebarcodes=false&translate-esc=false&unit=Fit&dpi=96&imagetype=Gif&rotation=0&color=%23000000&bgcolor=%23ffffff&qunit=Mm&quiet=0' />
                      <br />
                      <br />
                    </div>
                  </div>
                ";
                sb.Append(template);
            }
            return sb.ToString();
        }

        // private String Barcode(Ticket ticket)
        // {
        //     var barcode = new Barcode(ticket.Number, NetBarcode.Type.Code128, true);
        //     return barcode.GetBase64Image();
        // }
    }
}
