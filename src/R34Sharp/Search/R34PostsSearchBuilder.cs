using System.Text;

namespace R34Sharp
{
    /// <summary>
    /// A search builder for Rule34 Posts.
    /// </summary>
    public class R34PostsSearchBuilder
    {
        /// <summary>
        /// The limit of posts the API should return.
        /// </summary>
        /// <remarks>
        /// The value must be between 1 and 1000 posts.
        /// </remarks>
        public int Limit { get; set; }

        /// <summary>
        /// The Id of a specific Rule34 post.
        /// </summary>
        /// <remarks>
        /// This field is an optional value and if filled in, only one post will be returned.
        /// </remarks>
        public int Id { get; set; }

        /// <summary>
        /// The last post id returned by another Posts request with the same search builder.
        /// </summary>
        /// <remarks>
        /// This field is an optional value and if filled in, only posts subsequent to this post will be returned.
        /// </remarks>
        public int LastId { get; set; }

        /// <summary>
        /// The page for searching Posts.
        /// </summary>
        /// <remarks>
        /// If this value is filled in, pay attention to the <see cref="R34TagModel"/> of the search, as there may be inconsistency.
        /// </remarks>
        public int Page { get; set; }

        /// <summary>
        /// The tags that will be used for the search.
        /// </summary>
        public R34TagModel[] Tags { get; set; }

        /// <summary>
        /// The search may return deleted posts.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Build a custom search for Rule34 Posts.
        /// </summary>
        public R34PostsSearchBuilder()
        {
            Limit = 100;
            Id = 0;
            LastId = 0;
            Page = 0;
            Tags = Array.Empty<R34TagModel>();
            Deleted = false;
        }

        internal string GetTagsString()
        {
            StringBuilder tagsString = new();
            for (int i = 0; i < Tags.Length; i++)
            {
                R34TagModel tag = Tags[i];

                _ = tagsString.Append(tag.Name);
                if (i < Tags.Length - 1)
                {
                    _ = tagsString.Append('+');
                }
            }

            return tagsString.ToString();
        }
    }
}
