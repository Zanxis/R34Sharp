namespace R34Sharp
{
    /// <summary>
    /// A search builder for Rule34 Tags.
    /// </summary>
    public class R34TagsSearchBuilder
    {
        /// <summary>
        /// The exact name of the target tag being fetched.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The pattern that will be used for searching Tags.
        /// </summary>
        /// <remarks>
        /// The pattern causes Rule34 to return tags that have this pattern anywhere in the name.
        /// </remarks>
        public string NamePattern { get; set; }

        /// <summary>
        /// The ID of the tag being fetched.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The limit of tags that will be returned.
        /// </summary>
        /// <remarks>
        /// The value must be between 1 and 100.
        /// </remarks>
        public int Limit { get; set; }

        /// <summary>
        /// Build a custom search for Rule34 Tags.
        /// </summary>
        public R34TagsSearchBuilder()
        {
            Name = string.Empty;
            NamePattern = string.Empty;
            Limit = 100;
            Id = 0;
        }
    }
}
