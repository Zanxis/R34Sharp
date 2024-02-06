using R34Sharp.Entities.Posts;
using R34Sharp.Enums;
using R34Sharp.Models;
using R34Sharp.Search;

namespace R34Sharp.Experimental
{
    internal static class Program
    {
        private static string AssetsDirectory => Path.Combine(Directory.GetCurrentDirectory(), "R34Assets");

        private static readonly R34ApiClient _client = new();

        [MTAThread]
        private static async Task Main()
        {

            if (!Directory.Exists(AssetsDirectory))
            {
                _ = Directory.CreateDirectory(AssetsDirectory);
            }

            R34PostsSearchBuilder searchBuilder = new()
            {
                Limit = 100,
                Tags = new R34FormattedTag[]
                {
                    new("Bara"),
                    new("-Video"),
                },
            };

            Console.Clear();
            Console.WriteLine(" [ STARTING ] ");
            Console.WriteLine($"Path: {AssetsDirectory}");

            R34Posts posts = await _client.Posts.GetPostsAsync(searchBuilder);
            Console.WriteLine($"Request Completed!");

            int count = 0;
            foreach (R34Post post in posts.Data)
            {
                using MemoryStream ms = await post.DownloadFileAsync();
                byte[] r34FileByteArray = ms.ToArray();

                await File.WriteAllBytesAsync(Path.Combine(AssetsDirectory, $"{post.FileName}{post.FileExtension}"), r34FileByteArray);

                Console.WriteLine($"File #{count} Donwloaded! ({r34FileByteArray.Length / Math.Pow(1024, 2):0.###}mb)");
                count++;
            }
        }
    }
}