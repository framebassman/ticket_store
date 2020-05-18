using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace TicketStore.Api.Model.Payment.YandexMoney
{
    public class Validator
    {
        private ILogger<Validator> _log;

        public Validator(ILogger<Validator> log)
        {
            _log = log;
        }

        public virtual Boolean FromYandex(String notification_type, String operation_id, Decimal amount,
            String currency, DateTime datetime, String sender, Boolean codepro, String notification_secret,
            String label, String sha1Hash)
        {
            var uniqueTimezones = TimeZoneInfo.GetSystemTimeZones()
                .GroupBy(tz => tz.BaseUtcOffset)
                .Select(tz => tz.FirstOrDefault());
            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                _log.LogInformation("Check {0} timezone", timeZone);
                var fromYandex = fromTimezone(notification_type, operation_id, amount,
                    currency, datetime, sender, codepro, notification_secret,
                    label, sha1Hash, timeZone);
                if (fromYandex)
                {
                    return true;
                }
            }

            _log.LogInformation("Check all timezones - no matches");
            return false;
        }

        public Boolean fromTimezone(String notification_type, String operation_id, Decimal amount,
            String currency, DateTime datetime, String sender, Boolean codepro, String notification_secret,
            String label, String sha1Hash, TimeZoneInfo timezone)
        {

            var line = new StringBuilder()
                .Append(notification_type).Append("&")
                .Append(operation_id).Append("&")
                .Append(amount.ToString("0.00")).Append("&")
                .Append(currency).Append("&")
                .Append(ConvertToString(datetime, timezone)).Append("&")
                .Append(sender).Append("&")
                .Append(codepro.ToString().ToLower()).Append("&")
                .Append(notification_secret).Append("&")
                .Append(label)
                .ToString();
            _log.LogDebug("line was: {0}", line);
            var calculatedShaHash = shaHash(line);
            _log.LogDebug("calculated sha1Hash: {0}", calculatedShaHash);
            _log.LogDebug("origin sha1Hash: {0}", sha1Hash);
            return shaHash(line) == sha1Hash;
        }

        private String ConvertToString(DateTime origin, TimeZoneInfo timezone)
        {
            DateTime converted = TimeZoneInfo.ConvertTime(origin, timezone);
            string timezoneAppendix = timezone.DaylightName.Replace("GMT", "");
            return converted.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fff") + timezoneAppendix;
        }
        
        private string shaHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}