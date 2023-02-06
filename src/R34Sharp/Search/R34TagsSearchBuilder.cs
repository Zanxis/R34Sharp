namespace R34Sharp
{
    /// <summary>
    /// A search builder for Rule34 Tags.
    /// </summary>
    public class R34TagsSearchBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public required R34TagSearchType SearchType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Search { get; set; }

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
            SearchType = R34TagSearchType.Name;
            Search = string.Empty;
            Limit = 100;
        }
    }
}
