namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ProcessHelper" />.
    /// </summary>
    public static class ProcessHelper
    {
        #region Methods

        /// <summary>
        /// The OpenFolder.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void OpenFolder(string filePath)
        {
            var path = Path.GetDirectoryName(filePath);
            Process.Start(path);
        }

        /// <summary>
        /// The OpenUrl.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        public static void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url));
        }

        /// <summary>
        /// The Start.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void Start(string filePath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    ErrorDialog = true,
                    FileName = filePath,
                    Verb = "Open"
                }
            };
            process.Start();
        }

        #endregion
    }
}
