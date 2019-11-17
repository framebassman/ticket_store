using System;
using System.Text.Json;

namespace TicketStore.Api.Tests.Model.Services
{
    public abstract class Answer
    {
        public String message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
