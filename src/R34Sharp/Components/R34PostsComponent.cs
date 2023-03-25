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
        /// <exception cref="ArgumentException" />
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

            if (searchBuilder.Offset.HasValue) urlBuilder.AddParameter("pid", searchBuilder.Offset.Value.ToString());
            if (searchBuilder.Id.HasValue) urlBuilder.AddParameter("id", searchBuilder.Id.Value.ToString());

            // Get Result
            R34Posts postsResult = await GetAsync<R34Posts>(urlBuilder.Build(), _postsXmlSerializer);
            if (searchBuilder.BlockedTags.HasValue) postsResult.Data = postsResult.Data.Where(x => !x.HasTags(searchBuilder.BlockedTags.Value)).ToArray();

            return postsResult;
        }

        /// <summary>
        /// Get a complete list of Posts up to the selected limit based on a specified filter.
        /// </summary>
        /// <remarks>
        /// This method performs multiple searches until it finds all posts (specified in the search limit) or until there is no more content. Depending on the types of conditions selected the method can introduce significant delay and result in a possible TimeOut, so use it wisely.<br/><br/>
        /// If your filter is very trivial, it is recommended to use <see cref="GetPostsAsync"/>
        /// </remarks>
        /// <param name="searchBuilder">Search builder for Rule34 Posts.</param>
        /// <param name="filter">The filter that will be applied to each request result.</param>
        /// <returns>A collection of Rule34 posts.</returns>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="ArgumentException" />
        public async Task<R34Posts> GetPostsByFilterAsync(R34PostsSearchBuilder searchBuilder, Func<R34Post, bool> filter)
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
                searchBuilder.Offset = new(searchBuilder.Offset.Value + currentChunk);
                R34Posts posts = await GetPostsAsync(searchBuilder);

                if (posts == null || posts.Data == null || posts.Count == 0) break;

                foundPosts.AddRange(posts.Data.Where(filter));
                if (foundPosts.Count >= searchBuilder.Limit) break;

                currentChunk++;
            }

            // Return
            return new()
            {
                Data = foundPosts.Take(searchBuilder.Limit).ToArray(),
                Offset = (searchBuilder.Offset.Value + currentChunk) / 2,
            };
        }
    }
}