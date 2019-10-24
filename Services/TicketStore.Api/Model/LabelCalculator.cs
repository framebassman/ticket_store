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
            var time = _concert.Time;
            var result =
                $"{artist} {_longDash} {time.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"))}";
            _log.LogInformation("Label is {@result}", result);
            return result;
        }
    }
}