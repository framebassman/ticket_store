using Newtonsoft.Json;

namespace TicketStore.Api.Tests.Model.Services.Verify.Answers
{
    public abstract class Answer
    {
        public string message;
        
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
