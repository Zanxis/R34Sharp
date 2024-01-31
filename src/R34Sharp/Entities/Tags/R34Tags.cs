using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace R34Sharp.Entities.Tags
{
    /// <summary>
    /// Represents a collection of Rule34 tags.
    /// </summary>
    [XmlRoot(ElementName = "tags")]
    public sealed class R34Tags : R34Data
    {
        /// <summary>
        /// The collection of Rule34 tags.
        /// </summary>
        [XmlElement(ElementName = "tag")] public R34Tag[] Data { get; set; }

        /// <summary>
        /// The collection type.
        /// </summary>
        [XmlAttribute(AttributeName = "type")] public string Type { get; set; }

        /// <summary>
        /// The count of comments present in this collection.
        /// </summary>
        [XmlIgnore] public ulong Count => this.Data == null ? 0 : (ulong)this.Data.Length;

        internal override async Task BuildAsync(R34ApiClient instance)
        {
            if (this.Data == null)
            {
                return;
            }

            await Parallel.ForEachAsync(this.Data, new Func<R34Tag, CancellationToken, ValueTask>(async (current, token) =>
            {
                await current.BuildAsync(instance);
            }));
        }
    }
}
