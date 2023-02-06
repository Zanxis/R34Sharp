namespace R34Sharp
{
    internal static class FileHelpers
    {
        private static readonly string[] ImageExtensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp"
        };

        private static readonly string[] VideoExtensions =
        {
            ".mp4", ".mkv", ".avi", ".wmv", ".mov"
        };

        internal static FileMediaType GetMediaType(string extension)
        {
            string imageResult = Array.Find(ImageExtensions, x => x == extension);
            string videoResult = Array.Find(VideoExtensions, x => x == extension);

            return !string.IsNullOrEmpty(imageResult)
                ? FileMediaType.Image
                : !string.IsNullOrEmpty(videoResult) ? FileMediaType.Video : FileMediaType.Unknown;
        }
    }
}
