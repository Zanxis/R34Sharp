using System.Xml;
using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 post comment.
    /// </summary>
    [XmlRoot(ElementName = "comment")]
    public class R34Comment : R34Entity
    {
        #region HEADER
        /// <summary>
        /// Date and time the comment was published.
        /// </summary>
        [XmlIgnore] public DateTime CreatedAt { get; private set; }
        #endregion
        #region FILE
        /// <summary>
        /// The ID of comment.
        /// </summary>
        [XmlAttribute(AttributeName = "id")] public int Id { get; set; }

        /// <summary>
        /// The ID of the post.
        /// </summary>
        [XmlAttribute(AttributeName = "post_id")] public int PostId { get; set; }

        /// <summary>
        /// The ID of the comment creator.
        /// </summary>
        [XmlAttribute(AttributeName = "creator_id")] public int CreatorId { get; set; }

        /// <summary>
        /// The name of the comment creator.
        /// </summary>
        [XmlAttribute(AttributeName = "creator")] public string Creator { get; set; }

        /// <summary>
        /// Date and time the post was published string.
        /// </summary>
        [XmlAttribute(AttributeName = "created_at")] public string CreatedAtString { get; set; }

        /// <summary>
        /// The content of the comment.
        /// </summary>
        [XmlAttribute(AttributeName = "body")] public string Content { get; set; }
        #endregion

        /// <summary>
        /// </summary>
        protected override async Task OnBuildAsync()
        {
            CreatedAt = DateTimeHelpers.R34Parse(CreatedAtString, "yyyy-dd-MM mm:ss");
            await Task.CompletedTask;
        }
    }
}
