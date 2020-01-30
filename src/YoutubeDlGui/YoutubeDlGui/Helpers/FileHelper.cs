namespace YoutubeDlGui.Helpers
{
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="FileHelper" />
    /// </summary>
    public static class FileHelper
    {
        #region Methods

        /// <summary>
        /// The GetDirectorySize
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/></param>
        /// <returns>The <see cref="long"/></returns>
        public static long GetDirectorySize(string directory)
        {
            return Directory
                    .GetFiles(directory, "*.*", SearchOption.AllDirectories)
                    .Sum(f => new FileInfo(f).Length);
        }

        /// <summary>
        /// The GetDirectorySizeFormatted
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetDirectorySizeFormatted(string directory)
        {
            return FormatString.FormatFileSize(GetDirectorySize(directory));
        }

        /// <summary>
        /// Calculates the ETA (estimated time of accomplishment).
        /// </summary>
        /// <param name="speed">The speed as bytes. (Bytes per second?)</param>
        /// <param name="totalBytes">The total amount of bytes.</param>
        /// <param name="downloadedBytes">The amount of downloaded bytes.</param>
        /// <returns>The <see cref="long"/></returns>
        public static long GetETA(int speed, long totalBytes, long downloadedBytes)
        {
            if (speed == 0)
            {
                return 0;
            }

            var remainBytes = totalBytes - downloadedBytes;
            return remainBytes / speed;
        }

        /// <summary>
        /// Returns a long of the file size from given file in bytes.
        /// </summary>
        /// <param name="file">The file to get file size from.</param>
        /// <returns>The <see cref="long"/></returns>
        public static long GetFileSize(string file)
        {
            return !File.Exists(file) ? 0 : new FileInfo(file).Length;
        }

        /// <summary>
        /// Returns an formatted string of the given file's size.
        /// </summary>
        /// <param name="file">The file<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetFileSizeFormatted(string file) => FormatString.FormatFileSize(GetFileSize(file));

        #endregion Methods
    }
}