namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using Ookii.Dialogs.Wpf;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Helpers;
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
        /// <param name="downloadItemModels">The downloadItemModels<see cref="ICollection{DownloadItemModel}"/>.</param>
        public DownloadHandler(ICollection<DownloadItemModel> downloadItemModels)
        {
            this.DownloadItemModels = downloadItemModels;
            this.DownloadItemDict = new ConcurrentDictionary<int, DownloadProcessModel>();
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
        public IDictionary<int, DownloadProcessModel> DownloadItemDict { get; }

        /// <summary>
        /// Gets the DownloadItemModels.
        /// </summary>
        public ICollection<DownloadItemModel> DownloadItemModels { get; }

        /// <summary>
        /// Gets or sets the DownloadPath.
        /// </summary>
        public string DownloadPath { get; set; } = AppEnvironment.UserVideoFolder;

        /// <summary>
        /// Gets or sets a value indicating whether IsShowDialog.
        /// </summary>
        public bool IsShowDialog { get; set; } = true;

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
            lock (this.Lock)
            {
                if (this.DownloadItemDict.ContainsKey(downloadItem.Id) || callback.IsDisposed)
                {
                    return;
                }

                UIThreadHelper.Invoke(() =>
                {
                    using (callback)
                    {
                        var fileName = downloadItem.SuggestedFileName;
                        var filePath = Path.Combine(this.DownloadPath, downloadItem.SuggestedFileName);
                        if (this.IsShowDialog)
                        {
                            var dialog = new VistaSaveFileDialog
                            {
                                FileName = fileName,
                                CheckPathExists = true,
                                InitialDirectory = this.DownloadPath,
                                OverwritePrompt = true,
                                Title = "Save Link to...",
                            };

                            var element = chromiumWebBrowser as FrameworkElement;

                            var window = Window.GetWindow(element);
                            if (!(bool)dialog.ShowDialog(window))
                            {
                                return;
                            }

                            filePath = dialog.FileName;
                        }

                        this.DownloadPath = Path.GetDirectoryName(filePath);
                        var model = new DownloadProcessModel(downloadItem);
                        this.DownloadItemDict.Add(downloadItem.Id, model);
                        this.DownloadItemModels.Add(model);
                        callback.Continue(filePath, false);
                    }
                });
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
            lock (this.Lock)
            {
                var id = downloadItem.Id;
                if (!this.DownloadItemDict.TryGetValue(id, out DownloadProcessModel processModel))
                {
                    return;
                }

                if (processModel.IsCanceled || this.IsCancelAllDownloads)
                {
                    this.DownloadItemDict.Remove(id);
                    this.DownloadItemModels.Remove(processModel);
                    callback.Cancel();
                    return;
                }

                processModel.UpdateInfo(downloadItem);
            }
        }

        #endregion Methods
    }
}