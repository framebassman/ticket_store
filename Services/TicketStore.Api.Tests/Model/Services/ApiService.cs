using System;
using RestSharp;
using TicketStore.Api.Tests.Model.Services.UploadPoster.Requests;
using TicketStore.Api.Tests.Model.Services.Verify.Requests;

namespace TicketStore.Api.Tests.Model.Services
{
    public class ApiService : TicketStoreService
    {
        protected override int Port()
        {
            return 80;
        }

        public RestResponse SendTestPayment()
        {
            var request = new RestRequest("store_api/payments", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("test_notification", true);
            return Client.Execute(request);
        }

        public RestResponse SendPayment(String sender, YandexPaymentLabel label, String email, Decimal withdraw_amount, Decimal amount)
        {
            var request = new RestRequest("store_api/payments", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("sender", sender);
            request.AddParameter("label", label.Value());
            request.AddParameter("email", email);
            request.AddParameter("withdraw_amount", withdraw_amount);
            request.AddParameter("amount", amount);
            return Client.Execute(request);
        }

        public RestResponse VerifyBarcode(TurnstileScan scan)
        {
            var request = CreateVerifyRequest(scan);
            request.AddHeader("Authorization", "Bearer pkR9vfZ9QdER53mf");
            return Client.Execute(request);
        }

        public RestResponse VerifyBarcodeWithoutAuth(TurnstileScan scan)
        {
            var request = CreateVerifyRequest(scan);
            return Client.Execute(request);
        }

        private RestRequest CreateVerifyRequest(TurnstileScan scan)
        {
            var request = new RestRequest("store_api/verify", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(scan);
            return request;
        }

        public RestResponse UploadPoster(Poster poster)
        {
            var request = new RestRequest("store_api/uploads/poster", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(poster);
            return Client.Execute(request);
        }
    }
}
