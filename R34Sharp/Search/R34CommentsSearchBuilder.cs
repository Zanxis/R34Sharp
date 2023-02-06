namespace R34Sharp
{
    /// <summary>
    /// A search builder for Rule34 post comments.
    /// </summary>
    public class R34CommentsSearchBuilder
    {
        /// <summary>
        /// The ID of the post.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Build a custom search for Rule34 post comments.
        /// </summary>
        public R34CommentsSearchBuilder()
        {
            PostId = 0;
        }
    }
}
