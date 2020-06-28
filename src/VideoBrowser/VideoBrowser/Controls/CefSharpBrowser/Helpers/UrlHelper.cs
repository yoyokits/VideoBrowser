namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    using System;
    using System.Globalization;
    using System.Net;

    /// <summary>
    /// Defines the <see cref="UrlHelper" />.
    /// </summary>
    public static class UrlHelper
    {
        #region Methods

        /// <summary>
        /// The IsImageUrl.
        /// </summary>
        /// <param name="URL">The URL<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsImageUrl(this string URL)
        {
            if (!(HttpWebRequest.Create(URL) is HttpWebRequest req))
            {
                return false;
            }

            req.Method = "HEAD";
            try
            {
                using (var resp = req.GetResponse())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Methods
    }
}