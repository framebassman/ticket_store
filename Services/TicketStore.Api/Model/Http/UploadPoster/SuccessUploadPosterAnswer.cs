using System;

namespace TicketStore.Api.Model.Http
{
    public class SuccessUploadPosterAnswer : Answer
    {
        public String imageUrl;

        public SuccessUploadPosterAnswer(String uploadedImageUrl) : base("OK")
        {
            imageUrl = uploadedImageUrl;
        }
    }
}
