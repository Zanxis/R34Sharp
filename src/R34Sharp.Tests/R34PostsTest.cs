namespace R34Sharp.Tests
{
    public class R34PostsTest
    {
        private readonly R34ApiClient apiClient = new();

        [Fact]
        public async Task GetOnePostTestAsync()
        {
            R34Posts posts = await apiClient.GetPostsAsync(new()
            {
                Limit = 1
            });

            Assert.Equal(1, posts.Count);
        }

        [Fact]
        public async Task Get500PostsTestAsync()
        {
            R34Posts posts = await apiClient.GetPostsAsync(new()
            {
                Limit = 500
            });

            Assert.Equal(500, posts.Count);
        }

        [Fact]
        public async Task Get1000PostsTestAsync()
        {
            R34Posts posts = await apiClient.GetPostsAsync(new()
            {
                Limit = 1000
            });

            Assert.Equal(1000, posts.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(500)]
        [InlineData(1001)]
        public async Task SearchLimitTestAsync(int count)
        {
            await Assert.ThrowsAsync<IndexOutOfRangeException>(new(async () =>
            {
                await apiClient.GetPostsAsync(new()
                {
                    Limit = count,
                });
            }));
        }

        [Fact]
        public async Task GetPostWithSpecificTags()
        {
            R34TagModel testTag = new("1 Girl");

            R34Posts posts = await apiClient.GetPostsAsync(new()
            {
                Limit = 50,
                Tags = new R34TagModel[]
                {
                    testTag,
                },
            });

            int haveTags = 0;
            foreach (R34Post post in posts.Data)
            {
                if (Array.Find(post.Tags, x => x.Name == testTag.Name) != null)
                    haveTags++;
            }

            Assert.Equal(50, haveTags);
        }
    }
}