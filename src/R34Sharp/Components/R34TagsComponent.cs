using R34Sharp.Entities.Tags;
using R34Sharp.Enums;
using R34Sharp.Net;
using R34Sharp.Search;
using R34Sharp.Url;

using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R34Sharp.Components
{
    /// <summary>
    /// API component responsible for processes involving Rule34 tags chains.
    /// </summary>
    public sealed class R34TagsComponent : R34BaseApiComponent
    {
        private readonly XmlSerializer _tagsXmlSerializer = new(typeof(R34Tags));

        /// <summary>
        /// Get a list of Rule34 tags based on a <see cref="R34TagsSearchBuilder"/>.
        /// </summary>
        /// <param name="searchBuilder">Create a custom search to get tags and their information from Rule34.</param>
        /// <returns>A collection of Rule34 tags.</returns>
        /// <exception cref="ArgumentException" />
        public async Task<R34Tags> GetTagsAsync(R34TagsSearchBuilder searchBuilder)
        {
            UrlBuilder urlBuilder = new(R34Endpoints.INDEX);
            urlBuilder.AddParameter("page", "dapi");
            urlBuilder.AddParameter("s", "tag");
            urlBuilder.AddParameter("q", "index");
            urlBuilder.AddParameter("limit", searchBuilder.Limit.ToString());

            switch (searchBuilder.SearchType)
            {
                case R34TagSearchType.Name:
                    urlBuilder.AddParameter("name", searchBuilder.Search);
                    break;
                case R34TagSearchType.Pattern:
                    urlBuilder.AddParameter("name_pattern", searchBuilder.Search);
                    break;
                case R34TagSearchType.Id:
                    urlBuilder.AddParameter("id", searchBuilder.Search);
                    break;
                default:
                    urlBuilder.AddParameter("name", searchBuilder.Search);
                    break;
            }

            return await GetAsync<R34Tags>(urlBuilder.Build(), this._tagsXmlSerializer);
        }
    }
}
