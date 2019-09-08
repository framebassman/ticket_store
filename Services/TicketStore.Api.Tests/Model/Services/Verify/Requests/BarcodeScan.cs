namespace TicketStore.Api.Tests.Model.Services.Verify.Requests
{
    public class BarcodeScan : TurnstileScan
    {
        public BarcodeScan(string barcode) : base(barcode)
        {
            method = "Barcode";
        }
    }
}
