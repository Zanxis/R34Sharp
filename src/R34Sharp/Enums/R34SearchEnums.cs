namespace R34Sharp
{
    /// <summary>
    /// Search type by Rule34 Tags.
    /// </summary>
    public enum R34TagSearchType
    {
        /// <summary>
        /// Search by specific name.
        /// </summary>
        /// <remarks>
        /// When selected, a single Tag with the required name will be searched for and returned.
        /// </remarks>
        Name,

        /// <summary>
        /// Search by name pattern.
        /// </summary>
        /// <remarks>
        /// When selected, Tags that contain the search value in any part of their name will be searched for and returned.
        /// </remarks>
        Pattern,

        /// <summary>
        /// Search by ID.
        /// </summary>
        /// <remarks>
        /// When selected, a single tag will be searched and returned with the ID in Search.
        /// </remarks>
        Id
    }
}
