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
            WithSearchType(R34TagSearchType.Name);
            WithSearch(string.Empty);
            WithLimit(100);
        }

        public void WithSearchType(R34TagSearchType value)
        {
            SearchType = value;
        }
        public void WithSearch(string value)
        {
            Search = value;
        }
        public void WithLimit(int value)
        {
            Limit = value;
        }
    }
}
