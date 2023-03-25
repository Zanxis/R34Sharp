namespace R34Sharp
{
    /// <summary>
    /// A search builder for Rule34 Tags.
    /// </summary>
    public class R34TagsSearchBuilder
    {
        /// <summary>
        /// The type of search that will be performed on this browser.
        /// </summary>
        public required R34TagSearchType SearchType { get; set; }

        /// <summary>
        /// The fetch value of the respective tag.
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

        /// <summary>
        /// Set the search type.
        /// </summary>
        /// <param name="value">The search type.</param>
        /// <returns>This search builder.</returns>
        public R34TagsSearchBuilder WithSearchType(R34TagSearchType value)
        {
            SearchType = value;
            return this;
        }

        /// <summary>
        /// Set the search value.
        /// </summary>
        /// <param name="value">The search value.</param>
        /// <returns>This search builder.</returns>
        public R34TagsSearchBuilder WithSearch(string value)
        {
            Search = value;
            return this;
        }

        /// <summary>
        /// Set the serach limite.
        /// </summary>
        /// <param name="value">The serach limite.</param>
        /// <returns>This search builder.</returns>
        public R34TagsSearchBuilder WithLimit(int value)
        {
            Limit = value;
            return this;
        }
    }
}
