using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 API wrapper.
    /// </summary>
    /// <remarks>
    /// Responsible for all requests made to Rule34.
    /// </remarks>
    public class R34ApiClient : IDisposable
    {
        internal HttpClient ApiClient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public R34PostsComponent Posts { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public R34TagsComponent Tags { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public R34CommentsComponent Comments { get; private set; }

        /// <summary>
        /// Initializes the Rule34 Wrapper to enable communications between the website and the client.
        /// </summary>
        public R34ApiClient()
        {
            ApiClient = new()
            {
                BaseAddress = new(R34Endpoints.BASE_URI),
            };

            Posts = new();
            Tags = new();
            Comments = new();

            Posts.Build(this);
            Tags.Build(this);
            Comments.Build(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            ApiClient.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}