namespace R34Sharp
{
    /// <summary>
    /// Media type of a file.
    /// </summary>
    public enum R34FileType
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

    /// <summary>
    /// Represents a level of explicit content of the post.
    /// </summary>
    public enum R34Rating
    {
        /// <summary>
        /// Indicates that post contains G-rated, completely safe for work content.
        /// </summary>
        General,

        /// <summary>
        /// Indicates that post contains something not completely safe for work, or not completely safe to view in front of others.
        /// </summary>
        Safe,

        /// <summary>
        /// Indicates that post may contain some non-explicit nudity or sexual content, but isn't quite pornographic.
        /// </summary>
        Questionable,

        /// <summary>
        /// Indicates that post contains explicit sex, gratuitously exposed genitals, or it is otherwise pornographic.
        /// </summary>
        Explicit
    }
}
