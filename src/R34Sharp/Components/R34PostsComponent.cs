using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// API component responsible for processes involving Rule34 post chains.
    /// </summary>
    public sealed class R34PostsComponent : R34BaseApiComponent
    {
        private static readonly XmlSerializer _postsXmlSerializer = new(typeof(R34Posts));

        /// <summary>
        /// Get Rule34 posts based on a <see cref="R34PostsSearchBuilder"/>.
        /// </summary>
        /// <param name="searchBuilder">Search builder for Rule34 Posts.</param>
        /// <returns>A collection of Rule34 posts.</returns>
        /// <exception cref="IndexOutOfRangeException" />
        public async Task<R34Posts> GetPostsAsync(R34PostsSearchBuilder searchBuilder)
        {
            // Handler Exceptions
            if (searchBuilder.Limit < 1 || searchBuilder.Limit > 1000) await Task.FromException(new IndexOutOfRangeException("The limit allowed for obtaining Posts is a value between 1 and 1000."));
            if (searchBuilder.Tags == null || !searchBuilder.Tags.Any()) await Task.FromException(new ArgumentException("Search tags are missing."));

            // Build Url
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "post");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("limit", searchBuilder.Limit.ToString());
            urlBuilder.AddParameter("tags", searchBuilder.GetTagsString());

            if (searchBuilder.Chunk.HasValue) urlBuilder.AddParameter("pid", searchBuilder.Chunk.Value.ToString());
            if (searchBuilder.Id.HasValue) urlBuilder.AddParameter("id", searchBuilder.Id.Value.ToString());

            // Get Result
            return await GetAsync<R34Posts>(urlBuilder.Build(), _postsXmlSerializer);
        }

        /// <summary>
        /// Get a complete list of posts based on the type of media required, such as images or videos.
        /// </summary>
        /// <remarks>
        /// Unlike GetPostsAsync, this method will try to search for content indefinitely until it reaches the required file limit. For example, if you requested 1000 videos, this method will make requests indefinitely, in chunks of 1 post at a time, until it completes the 1000 videos or until there is no more content available. Due to its nature, this method may experience an imminent drop in performance or a longer time than normal to complete a request, use it wisely.
        /// </remarks>
        /// <param name="searchBuilder">Search builder for Rule34 Posts.</param>
        /// <param name="type">The required media file type.</param>
        /// <returns>A collection of Rule34 posts.</returns>
        public async Task<R34Posts> GetPostsByTypeAsync(R34PostsSearchBuilder searchBuilder, FileType type)
        {
            // Handler Exceptions
            if (searchBuilder.Limit < 1 || searchBuilder.Limit > 1000) await Task.FromException(new IndexOutOfRangeException("The limit allowed for obtaining Posts is a value between 1 and 1000."));
            if (searchBuilder.Tags == null || !searchBuilder.Tags.Any()) await Task.FromException(new ArgumentException("Search tags are missing."));

            // Posts
            List<R34Post> foundPosts = new();
            int currentChunk = 0;

            // Requests
            while (true)
            {
                searchBuilder.Chunk = new(searchBuilder.Chunk.Value + currentChunk);
                R34Posts posts = await GetPostsAsync(searchBuilder);

                if (posts == null || posts.Data == null || posts.Count == 0) break;

                foundPosts.AddRange(posts.Data.Where(x => x.FileType == type));
                if (foundPosts.Count >= searchBuilder.Limit) break;

                currentChunk++;
            }

            // Return
            return new()
            {
                Data = foundPosts.Take(searchBuilder.Limit).ToArray(),
                Offset = searchBuilder.Limit * (searchBuilder.Chunk.Value + currentChunk),
            };
        }
    }
}