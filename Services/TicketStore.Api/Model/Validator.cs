using System;
using System.Security.Cryptography;
using System.Text;

namespace TicketStore.Api.Model
{
    public class Validator
    {
        public virtual Boolean FromYandex(
            String notification_type,
            String operation_id,
            Decimal amount,
            String currency,
            DateTime datetime,
            String sender,
            Boolean codepro,
            String notification_secret,
            String label,
            String sha1Hash
        )
        {
            var line = new StringBuilder()
                .Append(notification_type).Append("&")
                .Append(operation_id).Append("&")
                .Append(amount.ToString("0.00")).Append("&")
                .Append(currency).Append("&")
                .Append(datetime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffzzz")).Append("&")
                .Append(sender).Append("&")
                .Append(codepro.ToString().ToLower()).Append("&")
                .Append(notification_secret).Append("&")
                .Append(label)
                .ToString();
            return shaHash(line) == sha1Hash;
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