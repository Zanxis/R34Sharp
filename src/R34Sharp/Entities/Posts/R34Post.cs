using System.Collections.Concurrent;
using System.Numerics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// A Rule34 post.
    /// </summary>
    [XmlRoot(ElementName = "post")]
    public class R34Post : R34Entity
    {
        /// <summary>
        /// The filename of the Post.
        /// </summary>
        [XmlIgnore] public string FileName { get; private set; }

        /// <summary>
        /// The file extension of the Post.
        /// </summary>
        [XmlIgnore] public string FileExtension { get; private set; }

        /// <summary>
        /// The Post file type.
        /// </summary>
        [XmlIgnore] public FileType FileType { get; private set; }

        /// <summary>
        /// Date and time the post was published.
        /// </summary>
        [XmlIgnore] public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Dimensions in pixels of the original Post file.
        /// </summary>
        [XmlIgnore] public Vector2 FileDimensions { get; private set; }

        /// <summary>
        /// Dimensions in pixels of the Post preview file.
        /// </summary>
        [XmlIgnore] public Vector2 PreviewFileDimensions { get; private set; }

        /// <summary>
        /// The post ID.
        /// </summary>
        [XmlAttribute(AttributeName = "id")] public ulong Id { get; set; }

        /// <summary>
        /// The ID of the parent post.
        /// </summary>
        [XmlAttribute(AttributeName = "parent_id")] public string ParentId { get; set; }

        /// <summary>
        /// The post creator ID.
        /// </summary>
        [XmlAttribute(AttributeName = "creator_id")] public ulong CreatorId { get; set; }

        /// <summary>
        /// Post file url.
        /// </summary>
        [XmlAttribute(AttributeName = "file_url")] public string FileUrl { get; set; }

        /// <summary>
        /// Post preview file url.
        /// </summary>
        [XmlAttribute(AttributeName = "preview_url")] public string PreviewUrl { get; set; }

        /// <summary>
        /// The Post score.
        /// </summary>
        [XmlAttribute(AttributeName = "score")] public int Score { get; set; }

        /// <summary>
        /// Indicative classification of the post.
        /// </summary>
        [XmlAttribute(AttributeName = "rating")] public string Rating { get; set; }

        /// <summary>
        /// Post file height.
        /// </summary>
        [XmlAttribute(AttributeName = "height")] public int Height { get; set; }

        /// <summary>
        /// Post file width.
        /// </summary>
        [XmlAttribute(AttributeName = "width")] public int Width { get; set; }

        /// <summary>
        /// Post Preview file width.
        /// </summary>
        [XmlAttribute(AttributeName = "preview_width")] public int PreviewWidth { get; set; }

        /// <summary>
        /// Post Preview file height.
        /// </summary>
        [XmlAttribute(AttributeName = "preview_height")] public int PreviewHeight { get; set; }

        /// <summary>
        /// Post flat file width.
        /// </summary>
        [XmlAttribute(AttributeName = "sample_width")] public int SampleWidth { get; set; }

        /// <summary>
        /// Post flat file height.
        /// </summary>
        [XmlAttribute(AttributeName = "sample_height")] public int SampleHeight { get; set; }

        /// <summary>
        /// Number of changes the post has had.
        /// </summary>
        [XmlAttribute(AttributeName = "change")] public int ChangesCount { get; set; }

        /// <summary>
        /// MD5 hash of the post.
        /// </summary>
        [XmlAttribute(AttributeName = "md5")] public string Md5 { get; set; }

        /// <summary>
        /// Post publication date and time string.
        /// </summary>
        [XmlAttribute(AttributeName = "created_at")] public string CreatedAtString { get; set; }

        /// <summary>
        /// The site from which the Post file originated.
        /// </summary>
        [XmlAttribute(AttributeName = "source")] public string SourceWebsite { get; set; }

        /// <summary>
        /// String of all Tags present in the Post.
        /// </summary>
        [XmlAttribute(AttributeName = "tags")] public string TagsString { get; set; }

        /// <summary>
        /// The post contains children.
        /// </summary>
        [XmlAttribute(AttributeName = "has_children")] public bool HasChildren { get; set; }

        /// <summary>
        /// The post contains notes.
        /// </summary>
        [XmlAttribute(AttributeName = "has_notes")] public bool HasNotes { get; set; }

        /// <summary>
        /// The post contains comments.
        /// </summary>
        [XmlAttribute(AttributeName = "has_comments")] public bool HasComments { get; set; }

        /// <summary>
        /// The status of the Post.
        /// </summary>
        [XmlAttribute(AttributeName = "status")] public string Status { get; set; }

        protected override async Task OnBuildAsync()
        {
            await SetFilesInfosAsync();
            await SetInfosAsync();

            await Task.CompletedTask;
        }
        private async Task SetFilesInfosAsync()
        {
            FileName = Path.GetFileNameWithoutExtension(FileUrl);
            FileExtension = Path.GetExtension(FileUrl);
            FileType = FileHelpers.GetMediaType(FileExtension);

            await Task.CompletedTask;
        }
        private async Task SetInfosAsync()
        {
            CreatedAt = DateTimeHelpers.R34Parse(CreatedAtString, "ddd MMM dd HH:mm:ss zzz yyyy");

            FileDimensions = new(Width, Height);
            PreviewFileDimensions = new(PreviewWidth, PreviewHeight);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Return all existing tags in the post as <see cref="R34TagModel"/> objects.
        /// </summary>
        /// <returns>
        /// Collection of post tags.
        /// </returns>
        public async Task<IEnumerable<R34TagModel>> GetTagsAsync()
        {
            string[] tagsArray = TagsString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ConcurrentBag<R34TagModel> tags = new();

            await Parallel.ForEachAsync(tagsArray, async (item, token) =>
            {
                await Task.Yield();

                R34TagModel tag = new(item);
                tags.Add(tag);
            });

            return tags.ToArray();
        }

        /// <summary>
        /// Checks if the post in question has the specified Tag.
        /// </summary>
        /// <param name="tag">The tag to fetch.</param>
        /// <returns>True if the Tag is found.</returns>
        public bool HasTag(R34TagModel tag)
        {
            return Array.Find(TagsString.Split(' ', StringSplitOptions.RemoveEmptyEntries), x => x == tag.Name) != null;
        }

        /// <summary>
        /// Checks that all specified tags exist in this post.
        /// </summary>
        /// <param name="tags">The tags to be fetch.</param>
        /// <returns>True if all Tags are found.</returns>
        public bool HasTags(R34TagModel[] tags)
        {
            foreach (R34TagModel tag in tags)
            {
                if (!HasTag(tag))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get all comments present on this Post.
        /// </summary>
        /// <returns>All comments on the post.</returns>
        /// <exception cref="InvalidOperationException" />
        public async Task<R34Comments> GetCommentsAsync()
        {
            if (HasComments)
                await Task.FromException(new InvalidOperationException("The post has no comments."));

            return await Task.FromResult(await R34Client.Comments.GetCommentsAsync(new() { PostId = Id }));
        }

        /// <summary>
        /// Download the post file.
        /// </summary>
        /// <returns>A <see cref="MemoryStream"/> containing the post file.</returns>
        public async Task<MemoryStream> DownloadFileAsync()
        {
            return await Task.FromResult(await DownloadAsync(FileUrl));
        }

        /// <summary>
        /// Download the preview post file.
        /// </summary>
        /// <returns>A <see cref="MemoryStream"/> containing the post preview file.</returns>
        public async Task<MemoryStream> DownloadFilePreviewAsync()
        {
            return await Task.FromResult(await DownloadAsync(PreviewUrl));
        }

        private async Task<MemoryStream> DownloadAsync(string url)
        {
            MemoryStream ms = new();

            // Get Stream
            using Stream fileStream = await R34Client.ApiClient.GetStreamAsync(url);
            await fileStream.CopyToAsync(ms);

            // Return Stream 
            return ms;
        }
    }
}