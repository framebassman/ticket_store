using System.Text.Json;

namespace TicketStore.Api.Tests.Unit.Model
{
    public static class Serializer
    {
        private static JsonSerializerOptions _options;
        
        static Serializer()
        {
            _options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public static string ToJson(object value)
        {
            return JsonSerializer.Serialize(value, _options);
        }
    }
}
