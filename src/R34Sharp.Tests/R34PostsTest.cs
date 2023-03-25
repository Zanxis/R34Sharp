namespace R34Sharp.Tests
{
    public class R34PostsTest
    {
        private static readonly R34ApiClient _client = new();
        private static readonly R34TagModel[] tagsPrefab = new R34TagModel[]
        {
            new("Little Mac"),
        };
        private static readonly R34TagModel[] blockedTagsPrefab = new R34TagModel[]
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

            }, x => x.FileType == FileType.Video);

            Assert.All(posts.Data, x => Assert.True(x.FileType == FileType.Video));
        }

        [Fact]
        public async Task Blocking_Posts_That_Contain_Certain_Tags_Async()
        {
            R34Posts posts = await _client.Posts.GetPostsAsync(new()
            {
                Limit = 1000,
                Tags = tagsPrefab,
                BlockedTags = new(blockedTagsPrefab)
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
                    Tags = Array.Empty<R34TagModel>()
                });
            });
        }
    }
}