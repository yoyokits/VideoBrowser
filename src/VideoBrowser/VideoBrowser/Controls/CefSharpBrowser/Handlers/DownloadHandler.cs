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
    using VideoBrowser.Core;
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
        public DownloadHandler(IList<DownloadItemModel> downloadItemModels)
        {
            this.DownloadItemModels = downloadItemModels;
            this.DownloadItemDict = new ConcurrentDictionary<int, DownloadProcessModel>();
            foreach (var downloadItem in this.DownloadItemModels)
            {
                downloadItem.RemoveDownloadAction = this.RemoveDownloadItem;
            }
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
        public IList<DownloadItemModel> DownloadItemModels { get; }

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
        /// The Download.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Download(Operation operation)
        {
            var operationModel = new OperationModel(operation)
            {
                CancelDownloadAction = this.OnCancelDownloadCalled,
                PauseDownloadAction = this.OnPauseDownloadCalled,
                RemoveDownloadAction = this.RemoveDownloadItem
            };
            if (!this.DownloadItemModels.Contains(operationModel))
            {
                // The list is connected with UI list therefore must be run in UI thread.
                UIThreadHelper.InvokeAsync(() =>
                {
                    this.DownloadItemModels.Insert(0, operationModel);
                    DownloadQueueHandler.Add(operation);
                });

                return string.Empty;
            }
            else
            {
                var output = Path.GetFileName(operationModel.Operation.Output);
                return $"The video/audio {output} is already downloaded";
            }
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
                        var model = new DownloadProcessModel(downloadItem) { RemoveDownloadAction = this.RemoveDownloadItem };
                        this.DownloadItemDict.Add(downloadItem.Id, model);
                        this.DownloadItemModels.Insert(0, model);
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

        /// <summary>
        /// The OnCancelDownloadCalled.
        /// </summary>
        /// <param name="model">The model<see cref="DownloadItemModel"/>.</param>
        private void OnCancelDownloadCalled(DownloadItemModel model)
        {
            model.Dispose();
            this.DownloadItemModels.Remove(model);
        }

        /// <summary>
        /// The OnPauseDownloadCalled.
        /// </summary>
        /// <param name="model">The model<see cref="DownloadItemModel"/>.</param>
        private void OnPauseDownloadCalled(DownloadItemModel model)
        {
            if (!(model is OperationModel operationModel))
            {
                return;
            }

            var operation = operationModel.Operation;
            var status = operation.Status;
            if (status != OperationStatus.Paused && status != OperationStatus.Queued && status != OperationStatus.Working)
            {
                return;
            }

            if (status == OperationStatus.Paused)
            {
                operation.Resume();
                model.PauseText = "Pause";
            }
            else
            {
                operation.Pause();
                model.PauseText = "Resume";
            }
        }

        /// <summary>
        /// The RemoveDownloadItem.
        /// </summary>
        /// <param name="model">The model<see cref="DownloadItemModel"/>.</param>
        private void RemoveDownloadItem(DownloadItemModel model)
        {
            lock (this.Lock)
            {
                if (!this.DownloadItemModels.Contains(model))
                {
                    return;
                }

                this.DownloadItemModels.Remove(model);
            }
        }

        #endregion Methods
    }
}