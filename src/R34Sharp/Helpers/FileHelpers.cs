using R34Sharp.Enums;

using System;

namespace R34Sharp.Helpers
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

        internal static R34FileType GetMediaType(string extension)
        {
            string imageResult = Array.Find(ImageExtensions, x => x == extension);
            string videoResult = Array.Find(VideoExtensions, x => x == extension);

            return !string.IsNullOrEmpty(imageResult)
                ? R34FileType.Image
                : !string.IsNullOrEmpty(videoResult) ? R34FileType.Video : R34FileType.Unknown;
        }
    }
}
