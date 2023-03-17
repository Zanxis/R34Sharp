using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// Represents a collection of Rule34 posts.
    /// </summary>
    [XmlRoot(ElementName = "posts")]
    public class R34Posts : R34Data
    {
        /// <summary>
        /// The collection of Rule34 posts.
        /// </summary>
        [XmlElement(ElementName = "post")] public R34Post[] Data { get; set; }

        /// <summary>
        /// The count of posts present in this collection.
        /// </summary>
        [XmlIgnore] public ulong Count => Data == null ? 0 : (ulong)Data.Length;

        /// <summary>
        /// The offset of current collection.
        /// </summary>
        [XmlAttribute(AttributeName = "offset")] public int Offset { get; set; }

        internal override async Task BuildAsync(R34ApiClient instance)
        {
            try
            {
                if (Data == null)
                    return;

                await Parallel.ForEachAsync(Data, new Func<R34Post, CancellationToken, ValueTask>(async (current, token) =>
                {
                    await current.BuildAsync(instance);
                }));
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }

            await Task.CompletedTask;
        }
    }
}
