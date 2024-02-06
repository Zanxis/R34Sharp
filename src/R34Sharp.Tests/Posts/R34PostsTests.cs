using R34Sharp.Entities.Posts;
using R34Sharp.Models;
using R34Sharp.Search;

namespace R34Sharp.Tests.Posts
{
    public class R34PostsTests
    {
        private static readonly R34ApiClient _client = new();
        private static readonly R34FormattedTag[] tagsPrefab = new R34FormattedTag[]
        {
            new("Little Mac"),
            new("-Looking At Viewer"),
        };

        [Fact]
        public async Task Get_One_Post_Async()
        {
            R34Posts posts = await _client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
            {
                Limit = 1,
                Tags = tagsPrefab
            });

            Assert.Equal<ulong>(1, posts.Count);
        }

        [Fact]
        public async Task Blocking_Posts_That_Contain_Certain_Tags_Async()
        {
            R34Posts posts = await _client.Posts.GetPostsAsync(new()
            {
                Limit = 1000,
                Tags = tagsPrefab,
            });

            Assert.All(posts.Data, x => Assert.False(x.HasTag(new("Looking At Viewer"))));
        }

        [Fact]
        public async Task Block_Requests_Outside_The_Limit_Range_Async()
        {
            _ = await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                _ = await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 0,
                    Tags = tagsPrefab
                });
            });

            _ = await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                _ = await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 1001,
                    Tags = tagsPrefab
                });
            });
        }

        [Fact]
        public async Task Block_Requests_Without_Tags_Async()
        {
            _ = await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                _ = await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 0,
                    Tags = Array.Empty<R34FormattedTag>()
                });
            });
        }
    }
}