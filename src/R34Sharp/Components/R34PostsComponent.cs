using R34Sharp.Entities.Posts;
using R34Sharp.Net;
using R34Sharp.Search;
using R34Sharp.Url;

using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R34Sharp.Components
{
    /// <summary>
    /// API component responsible for processes involving Rule34 post chains.
    /// </summary>
    public sealed class R34PostsComponent : R34BaseApiComponent
    {
        private readonly XmlSerializer _postsXmlSerializer = new(typeof(R34Posts));

        /// <summary>
        /// Get Rule34 posts based on a <see cref="R34PostsSearchBuilder"/>.
        /// </summary>
        /// <param name="searchBuilder">Search builder for Rule34 Posts.</param>
        /// <returns>A collection of Rule34 posts.</returns>
        /// <exception cref="ArgumentException" />
        public async Task<R34Posts> GetPostsAsync(R34PostsSearchBuilder searchBuilder)
        {
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "post");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("limit", searchBuilder.Limit.ToString());

            if (searchBuilder.Tags.Length > 0)
            {
                urlBuilder.AddParameter("tags", searchBuilder.GetTagsString());
            }

            if (searchBuilder.Offset.HasValue)
            {
                urlBuilder.AddParameter("pid", searchBuilder.Offset.Value.ToString());
            }

            if (searchBuilder.Id.HasValue)
            {
                urlBuilder.AddParameter("id", searchBuilder.Id.Value.ToString());
            }

            R34Posts postsResult = await GetAsync<R34Posts>(urlBuilder.Build(), this._postsXmlSerializer).ConfigureAwait(false);

            return postsResult;
        }
    }
}