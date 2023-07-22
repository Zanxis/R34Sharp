using System.Xml.Serialization;
using System.Net;

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
        internal HttpClient Client { get; private set; }

        /// <summary>
        /// Checks if this class has already been Disposable.
        /// </summary>
        public bool Disposable { get; private set; }

        /// <summary>
        /// API component responsible for posts.
        /// </summary>
        public R34PostsComponent Posts { get; private set; }

        /// <summary>
        /// API component responsible for tags.
        /// </summary>
        public R34TagsComponent Tags { get; private set; }

        /// <summary>
        /// API component responsible for comments.
        /// </summary>
        public R34CommentsComponent Comments { get; private set; }

        /// <summary>
        /// Initializes the Rule34 Wrapper to enable communications between the website and the client.
        /// </summary>
        public R34ApiClient()
        {
            StartClient();

            Posts = new();
            Tags = new();
            Comments = new();

            Posts.Build(this);
            Tags.Build(this);
            Comments.Build(this);
        }

        private void StartClient()
        {
            HttpClientHandler handler = new()
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };

            Client = new(handler)
            {
                BaseAddress = new(R34Endpoints.BASE_URI),
            };

            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 R34Sharp");
        }

        /// <summary>
        /// Dispose and initiate API client shutdown processes. In addition to relieving memory processes.
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
            Disposable = true;

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}