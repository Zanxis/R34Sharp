using R34Sharp.Components;
using R34Sharp.Net;

using System;
using System.Net;
using System.Net.Http;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 API wrapper.
    /// </summary>
    /// <remarks>
    /// Responsible for all requests made to Rule34.
    /// </remarks>
    public sealed class R34Client : IDisposable
    {
        internal HttpClient Client { get; private set; }

        /// <summary>
        /// Checks if this class has already been Disposable.
        /// </summary>
        public bool DisposedValue => this.disposedValue;

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

        private bool disposedValue;

        /// <summary>
        /// Initializes the Rule34 Wrapper to enable communications between the website and the client.
        /// </summary>
        public R34Client()
        {
            BuildClient();

            this.Posts = new();
            this.Tags = new();
            this.Comments = new();

            this.Posts.Build(this);
            this.Tags.Build(this);
            this.Comments.Build(this);
        }

        private void BuildClient()
        {
            HttpClientHandler handler = new()
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                MaxConnectionsPerServer = 10,
            };

            this.Client = new(handler)
            {
                BaseAddress = new(R34Endpoints.BASE_URI),
                Timeout = TimeSpan.FromSeconds(30),
            };

            this.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 R34Sharp");
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)this.Client).Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}