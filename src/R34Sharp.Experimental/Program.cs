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

            int count = 0;
            foreach (R34Post post in (await _client.Posts.GetPostsAsync(searchBuilder)).Data)
            {
                Console.WriteLine($"File #{count}: {post.FileName}{post.FileExtension}");
                count++;
            }
        }
    }
}