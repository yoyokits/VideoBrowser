namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using CefSharp;
    using VideoBrowser.Common;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="DownloadProcessModel" />.
    /// </summary>
    public class DownloadProcessModel : DownloadItemModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadProcessModel"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="DownloadItem"/>.</param>
        internal DownloadProcessModel(DownloadItem item)
        {
            this.Initialize(item);
            this.IsApplicationThumbnail = true;
            this.CancelDownloadCommand = new RelayCommand(this.OnCancelDownload, nameof(this.CancelDownloadCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether IsCanceled.
        /// </summary>
        internal bool IsCanceled { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The UpdateInfo.
        /// </summary>
        internal void UpdateInfo(DownloadItem downloadItem)
        {
            this.OutputPath = downloadItem.FullPath;
            this.Progress = downloadItem.PercentComplete;
            var speed = $"{downloadItem.CurrentSpeed.FormatFileSize()}/s";
            var percentComplete = $"{downloadItem.PercentComplete}%";
            var completeStatus = "Completed";
            this.Status = downloadItem.IsComplete ? completeStatus : $"{percentComplete} - {speed}";
            if (this.Status == completeStatus)
            {
                this.IsQueuedControlsVisible = false;
            }

            this.Thumbnail = this.OutputPath;
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize(DownloadItem downloadItem)
        {
            this.FileSize = FormatString.FormatFileSize(downloadItem.TotalBytes);
            this.Title = downloadItem.SuggestedFileName;
            this.Url = downloadItem.OriginalUrl;
            this.UpdateInfo(downloadItem);
        }

        /// <summary>
        /// The OnCancelDownload.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnCancelDownload(object obj)
        {
            this.IsCanceled = true;
        }

        #endregion Methods
    }
}