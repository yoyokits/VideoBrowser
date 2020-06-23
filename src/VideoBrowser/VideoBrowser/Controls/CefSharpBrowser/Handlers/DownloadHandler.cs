namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using System;

    /// <summary>
    /// Defines the <see cref="DownloadHandler" />.
    /// </summary>
    public class DownloadHandler : IDownloadHandler
    {
        #region Events

        /// <summary>
        /// Defines the OnBeforeDownloadFired.
        /// </summary>
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        /// <summary>
        /// Defines the OnDownloadUpdatedFired.
        /// </summary>
        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        #endregion Events

        #region Methods

        /// <summary>
        /// The OnBeforeDownload.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="downloadItem">The downloadItem<see cref="DownloadItem"/>.</param>
        /// <param name="callback">The callback<see cref="IBeforeDownloadCallback"/>.</param>
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            this.OnBeforeDownloadFired?.Invoke(this, downloadItem);

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        /// <summary>
        /// The OnDownloadUpdated.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="downloadItem">The downloadItem<see cref="DownloadItem"/>.</param>
        /// <param name="callback">The callback<see cref="IDownloadItemCallback"/>.</param>
        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            this.OnDownloadUpdatedFired?.Invoke(this, downloadItem);
        }

        #endregion Methods
    }
}