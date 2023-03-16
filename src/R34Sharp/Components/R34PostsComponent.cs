using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// 
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
    }
}