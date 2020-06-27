namespace VideoBrowser.Models
{
    using CefSharp;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
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
        public DownloadProcessModel(DownloadItem item)
        {
            this.DownloadItem = item;
            this.CancelDownloadCommand = new RelayCommand(this.OnCancelDownload, nameof(this.CancelDownloadCommand));
            this.Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadItem.
        /// </summary>
        internal DownloadItem DownloadItem { get; }

        /// <summary>
        /// Gets a value indicating whether IsCanceled.
        /// </summary>
        internal bool IsCanceled { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The UpdateInfo.
        /// </summary>
        internal void UpdateInfo()
        {
            this.Progress = this.DownloadItem.PercentComplete;
            var speed = $"{this.DownloadItem.CurrentSpeed.FormatFileSize()}/s";
            var percentComplete = $"{this.DownloadItem.PercentComplete}%";
            base.Status = this.DownloadItem.IsComplete ? "Completed" : $"{percentComplete} - {speed}";
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize()
        {
            this.FileSize = FormatString.FormatFileSize(this.DownloadItem.TotalBytes);
            this.OutputPath = this.DownloadItem.FullPath;
            this.Title = this.DownloadItem.SuggestedFileName;
            this.UpdateInfo();
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