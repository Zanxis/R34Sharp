using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 tag.
    /// </summary>
    [XmlRoot(ElementName = "tag")]
    public class R34Tag : R34Entity
    {
        #region BODY
        /// <summary>
        /// The tag name.
        /// </summary>
        [XmlAttribute(AttributeName = "name")] public string Name { get; set; }

        /// <summary>
        /// The ID of the tag.
        /// </summary>
        [XmlAttribute(AttributeName = "id")] public ulong Id { get; set; }

        /// <summary>
        /// Returns if the tag is ambiguous.
        /// </summary>
        [XmlAttribute(AttributeName = "ambiguous")] public bool Ambiguous { get; set; }

        /// <summary>
        /// Returns the tag type.
        /// </summary>
        [XmlAttribute(AttributeName = "type")] public ulong TypeId { get; set; }

        /// <summary>
        /// Count of posts with this tag.
        /// </summary>
        [XmlAttribute(AttributeName = "count")] public int PostsCount { get; set; }
        #endregion

        /// <summary>
        /// </summary>
        protected override async Task OnBuildAsync()
        {
            await Task.CompletedTask;
        }
    }
}
