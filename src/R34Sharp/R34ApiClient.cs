using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 API wrapper.
    /// </summary>
    /// <remarks>
    /// Responsible for all requests made to Rule34.
    /// </remarks>
    public class R34ApiClient
    {
        internal HttpClient ApiClient { get; set; }
        private readonly Dictionary<string, XmlSerializer> serializersPool = new()
        {
            ["Posts"] = new(typeof(R34Posts)),
            ["Comments"] = new(typeof(R34Comments)),
            ["Tags"] = new(typeof(R34Tags)),
        };

        /// <summary>
        /// Initializes the Rule34 Wrapper to enable communications between the website and the client.
        /// </summary>
        public R34ApiClient()
        {
            ApiClient = new()
            {
                BaseAddress = new(R34Endpoints.BASE_URI),
            };
        }

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

            // Build Url
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "post");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("limit", searchBuilder.Limit.ToString());
            urlBuilder.AddParameter("tags", searchBuilder.GetTagsString());

            if (searchBuilder.Deleted) urlBuilder.AddParameter("deleted", "show");
            if (searchBuilder.Chunk.HasValue) urlBuilder.AddParameter("pid", searchBuilder.Chunk.Value.ToString());
            if (searchBuilder.Id.HasValue) urlBuilder.AddParameter("id", searchBuilder.Id.Value.ToString());

            // Get Result
            return await GetAsync<R34Posts>(urlBuilder.Build(), "Posts");
        }

        /// <summary>
        /// Get comments for given Rule34 post based on a <see cref="R34CommentsSearchBuilder"/>.
        /// </summary>
        /// <param name="searchBuilder">Build a custom search to get comments from Rule34 Posts.</param>
        /// <returns>A collection of comments for a specific Rule34 posts.</returns>
        public async Task<R34Comments> GetCommentsAsync(R34CommentsSearchBuilder searchBuilder)
        {
            // Build Url
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "comment");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("post_id", searchBuilder.PostId.ToString());

            // Get Result
            return await GetAsync<R34Comments>(urlBuilder.Build(), "Comments");
        }

        /// <summary>
        /// Get a list of Rule34 tags based on a <see cref="R34TagsSearchBuilder"/>.
        /// </summary>
        /// <param name="searchBuilder">Create a custom search to get tags and their information from Rule34.</param>
        /// <returns>A collection of Rule34 tags.</returns>
        /// <exception cref="IndexOutOfRangeException" />
        public async Task<R34Tags> GetTagsAsync(R34TagsSearchBuilder searchBuilder)
        {
            // Handler Exceptions
            if (searchBuilder.Limit < 1 || searchBuilder.Limit > 100) await Task.FromException(new IndexOutOfRangeException("The limit allowed for obtaining Tags is a value between 1 and 100."));

            // Build Url
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "tag");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("limit", searchBuilder.Limit.ToString());

            switch (searchBuilder.SearchType)
            {
                case R34TagSearchType.Name: urlBuilder.AddParameter("name", searchBuilder.Search); break;
                case R34TagSearchType.Pattern: urlBuilder.AddParameter("name_pattern", searchBuilder.Search); break;
                case R34TagSearchType.Id: urlBuilder.AddParameter("id", searchBuilder.Search); break;
                default: urlBuilder.AddParameter("name", searchBuilder.Search); break;
            }

            // Get Result
            return await GetAsync<R34Tags>(urlBuilder.Build(), "Tags");
        }

        private async Task<T> GetAsync<T>(string url, string id) where T : R34Data
        {
            T result = default(T);

            // Creating the requisition
            using HttpResponseMessage response = await ApiClient.GetAsync(url);
            _ = response.EnsureSuccessStatusCode();

            // Receiving the values
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using StringReader reader = new(await response.Content.ReadAsStringAsync());

                    result = (T)serializersPool[id].Deserialize(reader);
                    await result.BuildAsync(this);
                }
                catch (Exception ex)
                {
                    await Task.FromException(ex);
                }
            }

            return await Task.FromResult(result);
        }
    }
}