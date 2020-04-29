using System.Linq;
using System.Text;

namespace TicketStore.Data.Parsers
{
    public class HerokuParser : JdbcParser
    {
        public HerokuParser(string origin) : base(origin)
        {
        }

        public override string Transform()
        {
            var candidate = base.Transform();
            var keys = candidate.Split(";").Where(k => !string.IsNullOrEmpty(k)).ToList();
            if (!keys.Contains("SSL Mode=Require"))
            {
                keys.Add("SSL Mode=Require");
            }

            if (!keys.Contains("Trust Server Certificate=true"))
            {
                keys.Add("Trust Server Certificate=true");
            }

            var builder = new StringBuilder();
            return builder.AppendJoin(";", keys).ToString();
        }
    }
}