using R34Sharp.Entities.Posts;
using R34Sharp.Models;
using R34Sharp.Search;
using R34Sharp.Enums;

namespace R34Sharp.Tests
{
    public class R34PostsTest
    {
        private static readonly R34Client _client = new();
        private static readonly R34FormattedTag[] tagsPrefab = new R34FormattedTag[]
        {
            new("Little Mac"),
        };
        private static readonly R34FormattedTag[] blockedTagsPrefab = new R34FormattedTag[]
        {
            new("Looking At Viewer"),
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
        public async Task Filter_All_Posts_To_Get_Videos_Async()
        {
            R34Posts posts = await _client.Posts.GetPostsByFilterAsync(new()
            {
                Limit = 1000,
                Tags = tagsPrefab

            }, x => x.FileType == R34FileType.Video);

            Assert.All(posts.Data, x => Assert.True(x.FileType == R34FileType.Video));
        }

        [Fact]
        public async Task Blocking_Posts_That_Contain_Certain_Tags_Async()
        {
            R34Posts posts = await _client.Posts.GetPostsAsync(new()
            {
                Limit = 1000,
                Tags = tagsPrefab,
                BlockedTags = blockedTagsPrefab,
            });

            Assert.All(posts.Data, x => Assert.False(x.HasTag(new("Looking At Viewer"))));
        }

        [Fact]
        public async Task Block_Requests_Outside_The_Limit_Range_Async()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 0,
                    Tags = tagsPrefab
                });
            });

            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 1001,
                    Tags = tagsPrefab
                });
            });
        }

        [Fact]
        public async Task Block_Requests_Without_Tags_Async()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await _client.Posts.GetPostsAsync(new()
                {
                    Limit = 0,
                    Tags = Array.Empty<R34FormattedTag>()
                });
            });
        }
    }
}