using System;
using Newtonsoft.Json;

namespace TicketStore.Api.Tests.Model.Services
{
    public abstract class Answer
    {
        public String message;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
