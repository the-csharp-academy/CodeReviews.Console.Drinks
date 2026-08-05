using SixPix;
using Spectre.Console;

namespace WebAPIClient.UserInterface
{
    public class ImageVisualisation
    {
        public static async Task GetImage(string imageUrl)
        {
            AnsiConsole.Clear();
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                imageUrl = "https://" + imageUrl.TrimStart('/');
            var bytes = await client.GetByteArrayAsync(imageUrl);

            using var imageStream = new MemoryStream(bytes);

            imageStream.Position = 0;
            ReadOnlySpan<char> sixelString = Sixel.Encode(imageStream);

            Console.Out.WriteLine(sixelString);
        }
    }
}
