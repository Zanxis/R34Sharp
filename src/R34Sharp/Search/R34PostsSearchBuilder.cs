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
        public required int Limit { get; set; }

        /// <summary>
        /// The Id of a specific Rule34 post.
        /// </summary>
        /// <remarks>
        /// This field is an optional value and if filled in, only one post will be returned.
        /// </remarks>
        public Optional<ulong> Id { get; set; }

        /// <summary>
        /// Get a specific chunk of posts from a given number.
        /// </summary>
        /// <remarks>
        /// To find a specific set of posts in a search, you can use "chunks" to find the posts following the number of chunks already searched. For example, if you've searched the last 1000 posts and want to get the next posts without overloading the search, you can set the number of "chunks" to "1" and so on. It's important to remember that "chunks" are directly related to the number of posts that will be searched. <br/><br/>
        /// If this value is filled in, pay attention to the <see cref="R34TagModel"/> of the search, as there may be inconsistency.
        /// </remarks>
        public Optional<int> Chunk { get; set; }

        /// <summary>
        /// The tags that will be used for the search.
        /// </summary>
        public required IEnumerable<R34TagModel> Tags { get; set; }

        /// <summary>
        /// Build a custom search for Rule34 Posts.
        /// </summary>
        public R34PostsSearchBuilder()
        {
            Limit = 100;
            Tags = Array.Empty<R34TagModel>();

            Id = new();
            Chunk = new();
        }

        internal string GetTagsString()
        {
            int length = Tags.Count();

            StringBuilder tagsString = new();
            for (int i = 0; i < length; i++)
            {
                R34TagModel tag = Tags.ElementAtOrDefault(i);
                if (tag == null) continue;

                _ = tagsString.Append(tag.Name);
                if (i < length - 1)
                {
                    _ = tagsString.Append('+');
                }
            }

            return tagsString.ToString();
        }
    }
}
