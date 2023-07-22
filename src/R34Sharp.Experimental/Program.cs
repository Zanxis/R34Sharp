using R34Sharp;

namespace R34Sharp.Experimental
{
    internal static class Program
    {
        private static string AssetsDirectory => Path.Combine(Directory.GetCurrentDirectory(), "R34Assets");

        private static R34ApiClient _client = new();

        [MTAThread]
        private static async Task Main() {

            if (!Directory.Exists(AssetsDirectory))
            {
                Directory.CreateDirectory(AssetsDirectory);
            }

            R34PostsSearchBuilder searchBuilder = new() {
                Limit = 100,
                Tags = new R34TagModel[]
                {
                    new("Bara"),
                },
            };

            Console.Clear();
            Console.WriteLine(" [ STARTING ] ");
            Console.WriteLine($"Path: {AssetsDirectory}");

            int count = 0;
            foreach (R34Post post in (await _client.Posts.GetPostsByFilterAsync(searchBuilder, x => x.Rating == R34Rating.Questionable)).Data)
            {
                using MemoryStream ms = await post.DownloadFileAsync();
                byte[] r34FileByteArray = ms.ToArray();

                await File.WriteAllBytesAsync(Path.Combine(AssetsDirectory, $"{post.FileName}{post.FileExtension}"), r34FileByteArray);

                Console.WriteLine($"File #{count} Donwloaded! ({(r34FileByteArray.Length / (Math.Pow(1024, 2))).ToString("0.###")}mb)");
                count++;
            }
        }
    }
}