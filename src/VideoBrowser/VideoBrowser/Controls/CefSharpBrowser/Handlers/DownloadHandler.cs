namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using VideoBrowser.Models;

    /// <summary>
    /// Defines the <see cref="DownloadHandler" />.
    /// </summary>
    public class DownloadHandler : IDownloadHandler, IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadHandler"/> class.
        /// </summary>
        public DownloadHandler()
        {
            this.DownloadItemDict = new ConcurrentDictionary<DownloadItem, DownloadProcessModel>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether Disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Gets the DownloadItemDict.
        /// </summary>
        public IDictionary<DownloadItem, DownloadProcessModel> DownloadItemDict { get; }

        /// <summary>
        /// Gets or sets a value indicating whether IsCancelAllDownloads.
        /// </summary>
        private bool IsCancelAllDownloads { get; set; }

        /// <summary>
        /// Gets the Lock.
        /// </summary>
        private object Lock { get; } = new object();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.Disposed)
            {
                return;
            }

            this.Disposed = true;
        }

        /// <summary>
        /// The OnBeforeDownload.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="downloadItem">The downloadItem<see cref="DownloadItem"/>.</param>
        /// <param name="callback">The callback<see cref="IBeforeDownloadCallback"/>.</param>
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            this.OnBeforeDownloadFired(downloadItem);

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
            if (!this.DownloadItemDict.TryGetValue(downloadItem, out DownloadProcessModel processModel))
            {
                return;
            }

            if (processModel.IsCanceled || this.IsCancelAllDownloads)
            {
                callback.Cancel();
            }
        }

        /// <summary>
        /// The OnBeforeDownloadFired.
        /// </summary>
        /// <param name="downloadItem">The downloadItem<see cref="DownloadItem"/>.</param>
        private void OnBeforeDownloadFired(DownloadItem downloadItem)
        {
            lock (this.Lock)
            {
                if (this.DownloadItemDict.ContainsKey(downloadItem))
                {
                    return;
                }

                var model = new DownloadProcessModel(downloadItem);
                this.DownloadItemDict.Add(downloadItem, model);
            }
        }

        /// <summary>
        /// The OnDownloadHandler_OnBeforeDownloadFired.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="DownloadItem"/>.</param>
        private void OnDownloadHandler_OnBeforeDownloadFired(object sender, DownloadItem e)
        {
        }

        #endregion Methods
    }
}