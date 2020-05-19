namespace VideoBrowser.Helpers
{
    using System;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="UrlHelper" />.
    /// </summary>
    internal static class UrlHelper
    {
        #region Methods

        /// <summary>
        /// The GetValidUrl.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetValidUrl(string url)
        {
            if (!IsValidUrl(url))
            {
                var encodedUrl = HttpUtility.UrlEncode(url);
                url = $"https://www.youtube.com/results?search_query={encodedUrl}";
            }

            return url;
        }

        /// <summary>
        /// The IsValidUrl.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidUrl(string url)
        {
            if (!url.Contains("http"))
            {
                url = $"http://{url}";
            }

            var result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        #endregion Methods
    }
}