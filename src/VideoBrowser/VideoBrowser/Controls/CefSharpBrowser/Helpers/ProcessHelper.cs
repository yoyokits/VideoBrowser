namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

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
        public static void OpenContainedFolder(string filePath)
        {
            var path = Path.GetDirectoryName(filePath);
            Start(path);
        }

        /// <summary>
        /// The OpenUrl.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        public static void OpenUrl(string url)
        {
            Start(url);
        }

        /// <summary>
        /// The Start.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public static void Start(string filePath)
        {
            Task.Run(() =>
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
            });
        }

        #endregion Methods
    }
}