using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model
{
    public class LabelCalculator
    {
        private ILogger _log;
        private Event _concert;
        private String _longDash = "—";

        public LabelCalculator(ILogger log, Event concert)
        {
            _log = log;
            _concert = concert;
        }

        public String Value()
        {
            var artist = _concert.Artist;
            var time = translateYearToRussian(
                _concert.Time.ToString(
                        "d MMMM yyyy",
                        CultureInfo.CreateSpecificCulture("ru")
                        )
                .ToLower()
            );
            var result = $"{artist} {_longDash} {time}";
            _log.LogInformation("Label is {@result}", result);
            return result;
        }

        private String translateYearToRussian(String origin)
        {
            origin = origin.Replace("January", "Января");
            origin = origin.Replace("january", "января");
            origin = origin.Replace("February", "Февраля");
            origin = origin.Replace("february", "февраля");
            origin = origin.Replace("March", "Марта");
            origin = origin.Replace("march", "марта");
            origin = origin.Replace("April", "Апреля");
            origin = origin.Replace("april", "апреля");
            origin = origin.Replace("May", "Мая");
            origin = origin.Replace("may", "мая");
            origin = origin.Replace("June", "Июня");
            origin = origin.Replace("june", "июня");
            origin = origin.Replace("July", "Июля");
            origin = origin.Replace("july", "июля");
            origin = origin.Replace("August", "Августа");
            origin = origin.Replace("august", "августа");
            origin = origin.Replace("September", "Сентября");
            origin = origin.Replace("september", "cентября");
            origin = origin.Replace("October", "Октября");
            origin = origin.Replace("october", "октября");
            origin = origin.Replace("November", "Ноября");
            origin = origin.Replace("november", "ноября");
            origin = origin.Replace("December", "Декабря");
            origin = origin.Replace("december", "декабря");
            return origin;
        }
    }
}