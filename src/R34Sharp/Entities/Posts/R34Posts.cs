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
        [XmlAttribute(AttributeName = "count")] public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(AttributeName = "offset")] public int Offset { get; set; }

        internal override async Task BuildAsync(R34ApiClient instance)
        {
            List<Task> tasks = new();
            foreach (R34Post post in Data)
            {
                tasks.Add(Task.Run(() => post.BuildAsync(instance)));
            }
            Task.WaitAll(tasks.ToArray());

            await Task.CompletedTask;
        }
    }
}
