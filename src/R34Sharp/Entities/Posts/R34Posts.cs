using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace R34Sharp.Entities.Posts
{
    /// <summary>
    /// Represents a collection of Rule34 posts.
    /// </summary>
    [XmlRoot(ElementName = "posts")]
    public sealed class R34Posts : R34Data
    {
        /// <summary>
        /// The collection of Rule34 posts.
        /// </summary>
        [XmlElement(ElementName = "post")] public R34Post[] Data { get; set; }

        /// <summary>
        /// The count of posts present in this collection.
        /// </summary>
        [XmlIgnore] public ulong Count => this.Data == null ? 0 : (ulong)this.Data.Length;

        /// <summary>
        /// The offset of current collection.
        /// </summary>
        [XmlAttribute(AttributeName = "offset")] public int Offset { get; set; }

        internal override async Task BuildAsync(R34ApiClient instance)
        {
            if (this.Data == null)
            {
                return;
            }

            await Parallel.ForEachAsync(this.Data, new Func<R34Post, CancellationToken, ValueTask>(async (current, token) =>
            {
                await current.BuildAsync(instance);
            }));
        }
    }
}
