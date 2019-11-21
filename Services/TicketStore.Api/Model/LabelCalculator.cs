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
                _concert.Time.ToString().ToLower()
            );
            var result = $"{artist} {_longDash} {time}";
            _log.LogInformation("Label is {@result}", result);
            return result;
        }

        public String translateMonthToRussian(String origin)
        {
            origin.Replace("january", "января");
            origin.Replace("february", "февраля");
            origin.Replace("march", "марта");

            origin.Replace("april", "апреля");
            origin.Replace("may", "мая");
            origin.Replace("june", "июня");

            origin.Replace("july", "июля");
            origin.Replace("august", "августа");
            origin.Replace("september", "сентября");

            origin.Replace("october", "октября");
            origin.Replace("november", "ноября");
            origin.Replace("december", "декабря");
            return origin;
        }
    }
}
