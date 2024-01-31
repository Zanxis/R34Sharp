using R34Sharp.Entities.Posts;
using R34Sharp.Net;
using R34Sharp.Search;
using R34Sharp.Url;

using System;
using System.Collections.Generic;
using System.Linq;
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

            if (searchBuilder.BlockedTags.Length > 0)
            {
                postsResult.Data = postsResult.Data.Where(x => !x.HasTags(searchBuilder.BlockedTags)).ToArray();
            }

            return postsResult;
        }

        /// <summary>
        /// Get a complete list of Posts up to the selected limit based on a specified filter.
        /// </summary>
        /// <remarks>
        /// This method performs multiple searches until it finds all posts (specified in the search limit) or until there is no more content. Depending on the types of conditions selected the method can introduce significant delay and result in a possible TimeOut, so use it wisely.<br/><br/>
        /// If your filter is very trivial, it is recommended to use <see cref="GetPostsAsync"/>.
        /// </remarks>
        /// <param name="searchBuilder">Search builder for Rule34 Posts.</param>
        /// <param name="filter">The filter that will be applied to each request result.</param>
        /// <returns>A collection of Rule34 posts.</returns>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="ArgumentException" />
        public async Task<R34Posts> GetPostsByFilterAsync(R34PostsSearchBuilder searchBuilder, Func<R34Post, bool> filter)
        {
            List<R34Post> foundPosts = new();

            // Requests
            int currentChunk = 0;
            int currentOffset = 0;

            while (true)
            {
                currentOffset += currentChunk;
                R34Posts posts = await GetPostsAsync(searchBuilder).ConfigureAwait(false);

                if (posts == null || posts.Data == null || posts.Count == 0)
                {
                    break;
                }

                foundPosts.AddRange(posts.Data.Where(filter));
                if (foundPosts.Count >= searchBuilder.Limit)
                {
                    break;
                }

                currentChunk++;
            }

            // Return
            return new R34Posts
            {
                Data = foundPosts.Take(searchBuilder.Limit).ToArray(),
                Offset = (currentOffset + currentChunk) / 2,
            };
        }
    }
}