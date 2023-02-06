namespace R34Sharp
{
    /// <summary>
    /// Media type of a file.
    /// </summary>
    public enum FileMediaType
    {
        /// <summary>
        /// File with unknown extension or not registered in the API
        /// </summary>
        Unknown,

        /// <summary>
        /// An image file.
        /// </summary>
        /// <remarks>
        /// Includes PNG, JPG, JPEG, GIF and BMP files.
        /// </remarks>
        Image,

        /// <summary>
        /// An video file.
        /// </summary>
        /// <remarks>
        /// Includes MP4, MKV, AVI, WMV and MOV files.
        /// </remarks>
        Video
    }
}
