using System.Xml.Serialization;

namespace R34Sharp
{
    public sealed class R34CommentsComponent : R34BaseApiComponent
    {
        private readonly XmlSerializer _commentsXmlSerializer = new(typeof(R34Comments));

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
            return await GetAsync<R34Comments>(urlBuilder.Build(), _commentsXmlSerializer);
        }


    }
}
