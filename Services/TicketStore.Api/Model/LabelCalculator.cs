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
            var time = translateMonthToRussian(
                _concert.Time
                    .ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))
                    .ToLower()
            );
            var result = $"{artist} {_longDash} {time}";
            _log.LogInformation("Label is {@result}", result);
            return result;
        }

        public String translateMonthToRussian(String origin)
        {
            origin = origin.Replace("january", "января");
            origin = origin.Replace("february", "февраля");
            origin = origin.Replace("march", "марта");

            origin = origin.Replace("april", "апреля");
            origin = origin.Replace("may", "мая");
            origin = origin.Replace("june", "июня");

            origin = origin.Replace("july", "июля");
            origin = origin.Replace("august", "августа");
            origin = origin.Replace("september", "сентября");

            origin = origin.Replace("october", "октября");
            origin = origin.Replace("november", "ноября");
            origin = origin.Replace("december", "декабря");
            return origin;
        }
    }
}
