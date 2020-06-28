namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using System;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="DownloadItemModel" />.
    /// </summary>
    public abstract class DownloadItemModel : NotifyPropertyChanged, IDisposable
    {
        #region Fields

        private string _fileSize;

        private bool _isQueuedControlsVisible = true;

        private string _outputPath;

        private string _pauseText = "Pause";

        private int _progress;

        private string _status;

        private string _thumbnail;

        private string _title;

        private string _url;

        private bool Disposed = false;// To detect redundant calls

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadItemModel"/> class.
        /// </summary>
        protected DownloadItemModel()
        {
            this.ExecuteDownloadedCommand = new RelayCommand(this.OnExecuteDownloaded, nameof(this.ExecuteDownloadedCommand));
            this.ShowDownloadedFolderCommand = new RelayCommand(this.OnShowDownloadedFolderCommand, nameof(this.ShowDownloadedFolderCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the CancelDownloadCommand.
        /// </summary>
        public ICommand CancelDownloadCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the DownloadPath.
        /// </summary>
        public string DownloadPath { get; protected set; }

        /// <summary>
        /// Gets or sets the ExecuteDownloadedCommand.
        /// </summary>
        public ICommand ExecuteDownloadedCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the FileSize.
        /// </summary>
        public string FileSize { get => this._fileSize; protected set => this.Set(this.PropertyChangedHandler, ref this._fileSize, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsApplicationThumbnail.
        /// </summary>
        public bool IsApplicationThumbnail { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether IsCompletedControlsVisible.
        /// </summary>
        public bool IsCompletedControlsVisible { get => !this.IsQueuedControlsVisible; }

        /// <summary>
        /// Gets or sets a value indicating whether IsQueuedControlsVisible.
        /// </summary>
        public bool IsQueuedControlsVisible
        {
            get => _isQueuedControlsVisible;
            internal set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _isQueuedControlsVisible, value))
                {
                    return;
                }

                this.OnPropertyChanged(nameof(this.IsCompletedControlsVisible));
            }
        }

        /// <summary>
        /// Gets or sets the OutputPath.
        /// </summary>
        public string OutputPath { get => _outputPath; protected set => this.Set(this.PropertyChangedHandler, ref _outputPath, value); }

        /// <summary>
        /// Gets or sets the PauseDownloadCommand.
        /// </summary>
        public ICommand PauseDownloadCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the PauseText.
        /// </summary>
        public string PauseText { get => _pauseText; internal set => this.Set(this.PropertyChangedHandler, ref _pauseText, value); }

        /// <summary>
        /// Gets or sets the Progress.
        /// </summary>
        public int Progress { get => this._progress; protected set => this.Set(this.PropertyChangedHandler, ref this._progress, value); }

        /// <summary>
        /// Gets or sets the ShowDownloadedFolderCommand.
        /// </summary>
        public ICommand ShowDownloadedFolderCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string Status { get => this._status; protected set => this.Set(this.PropertyChangedHandler, ref this._status, value); }

        /// <summary>
        /// Gets or sets the Thumbnail.
        /// </summary>
        public string Thumbnail { get => _thumbnail; protected set => this.Set(this.PropertyChangedHandler, ref _thumbnail, value); }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get => this._title; protected set => this.Set(this.PropertyChangedHandler, ref this._title, value); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => this._url; protected set => this.Set(this.PropertyChangedHandler, ref this._url, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="DownloadItemModel"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(DownloadItemModel other)
        {
            var isEqual = other != null && this.OutputPath == other.OutputPath;
            return isEqual;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                this.Disposed = true;
            }
        }

        /// <summary>
        /// The OnPlayMedia.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnExecuteDownloaded(object obj)
        {
            ProcessHelper.Start(this.OutputPath);
        }

        /// <summary>
        /// The OnShowMediaInFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnShowDownloadedFolderCommand(object obj)
        {
            ProcessHelper.OpenFolder(this.OutputPath);
        }

        #endregion Methods
    }
}