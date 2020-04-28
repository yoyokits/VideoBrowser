namespace YoutubeDlGui.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using YoutubeDlGui.Core;

    /// <summary>
    /// Defines the <see cref="YoutubeHelper" />.
    /// </summary>
    public static class YoutubeHelper
    {
        #region Methods

        /// <summary>
        /// Returns a fixed URL, stripped of unnecessary invalid information.
        /// </summary>
        /// <param name="url">The URL to fix.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string FixUrl(string url)
        {
            // Remove "Watch Later" information, causes error
            url = url.Replace("&index=6&list=WL", "");
            return url;
        }

        /// <summary>
        /// Returns a formatted string of the given title, stripping illegal characters and replacing HTML entities with their actual character. (e.g. &quot; -> ').
        /// </summary>
        /// <param name="title">The title to format.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string FormatTitle(string title)
        {
            var illegalCharacters = new string[] { "/", @"\", "*", "?", "\"", "<", ">" };
            var replace = new Dictionary<string, string>()
            {
                {"|", "-"},
                {"&#39;", "'"},
                {"&quot;", "'"},
                {"&lt;", "("},
                {"&gt;", ")"},
                {"+", " "},
                {":", "-"},
                {"amp;", "&"}
            };

            var sb = new System.Text.StringBuilder(title);
            foreach (string s in illegalCharacters)
            {
                sb.Replace(s, string.Empty);
            }

            foreach (KeyValuePair<string, string> s in replace)
            {
                sb.Replace(s.Key, s.Value);
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Returns the highest quality audio format from the given VideoFormat.
        /// </summary>
        /// <param name="format">The format to get audio format from.</param>
        /// <returns>The <see cref="VideoFormat"/>.</returns>
        public static VideoFormat GetAudioFormat(VideoFormat format)
        {
            var audio = new List<VideoFormat>();

            // Add all audio only formats
            audio.AddRange(format.VideoInfo.Formats.FindAll(f => f.AudioOnly == true && f.Extension != "webm"));

            // Return null if no audio is found
            if (audio.Count == 0)
            {
                return null;
            }

            // Return either the one with the highest audio bit rate, or the last found one
            return audio.OrderBy(a => a.AudioBitRate).Last();
        }

        /// <summary>
        /// Returns the playlist id from given url.
        /// </summary>
        /// <param name="url">The url to get playlist id from.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetPlaylistId(string url)
        {
            var regex = new Regex(@"^(?:https?://)?(?:www.)?youtube.com/.*list=([0-9a-zA-Z\-_]*).*$");
            return regex.Match(url).Groups[1].Value;
        }

        /// <summary>
        /// Returns true if the given url is a playlist YouTube url.
        /// </summary>
        /// <param name="url">The url to check.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsPlaylist(string url)
        {
            var regex = new Regex(@"^(?:https?://)?(?:www.)?youtube.com/.*list=([0-9a-zA-Z\-_]*).*$");
            return regex.IsMatch(url);
        }

        /// <summary>
        /// Returns true if the given url is a valid YouTube url.
        /// </summary>
        /// <param name="url">The url to check.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidYouTubeUrl(string url)
        {
            if (!url.ToLower().Contains("www.youtube.com/watch?"))
            {
                return false;
            }

            var pattern = @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$";
            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.IsMatch(url);
        }

        /// <summary>
        /// The CheckFormats.
        /// </summary>
        /// <param name="list">The list<see cref="IList{VideoFormat}"/>.</param>
        /// <returns>The <see cref="VideoFormat[]"/>.</returns>
        internal static VideoFormat[] CheckFormats(IList<VideoFormat> list)
        {
            var formats = new List<VideoFormat>(list.Distinct());
            formats.RemoveAll(f => f.Extension.Contains("webm") ||
                                   f.HasAudioAndVideo ||
                                   f.FormatID == "meta");

            return formats.ToArray();
        }

        #endregion Methods
    }
}