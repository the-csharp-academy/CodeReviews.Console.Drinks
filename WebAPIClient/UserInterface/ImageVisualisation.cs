using Spectre.Console;

namespace WebAPIClient.UserInterface
{
    public class ImageVisualisation
    {
        public static async Task<CanvasImage> GetImage(string imageUrl)
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                imageUrl = "https://" + imageUrl.TrimStart('/');
            var bytes = await client.GetByteArrayAsync(imageUrl);

            using var stream = new MemoryStream(bytes);
            var image = new CanvasImage(stream)
                .BicubicResampler()
                .MaxWidth(20);
            return image;
        }
    }
}
