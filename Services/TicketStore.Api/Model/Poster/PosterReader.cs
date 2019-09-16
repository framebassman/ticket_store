using System.IO;
using System.Net;
using ImageMagick;

namespace TicketStore.Api.Model.Poster
{
    public class PosterReader : IPosterReader
    {
        public PosterReader()
        {
        }

        public byte[] GetImage(Poster poster)
        {
            var imageUrl = poster.imageUrl;
            using (var webClient = new WebClient())
            {
                using (var inputStream = webClient.OpenRead(imageUrl))
                {
                    using (MagickImage outputImage = new MagickImage(inputStream))
                    {
                        outputImage.Format = MagickFormat.Jpeg;
                        MagickGeometry size = new MagickGeometry(1000, 1000);
                        outputImage.Resize(size);

                        var byteArray = outputImage.ToByteArray();
                        var compressedStream = new MemoryStream(byteArray);
                        ImageOptimizer optimizer = new ImageOptimizer();
                        optimizer.Compress(compressedStream);

                        using (MagickImage compressedImage = new MagickImage(compressedStream))
                        {
                            return compressedImage.ToByteArray();
                        }
                    }
                }
            }
        }
    }
}
