namespace VideoBrowser.Models
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="OperationModel" />
    /// The Download Queue Items.
    /// </summary>
    public class OperationModel : NotifyPropertyChanged, IEquatable<OperationModel>, IDisposable
    {
        #region Fields

        private string _duration;

        private string _fileSize;

        private bool _isQueuedControlsVisible = true;

        private string _pauseText = "Pause";

        private int _progress;

        private string _status;

        private string _title;

        private string _url;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationModel"/> class.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        public OperationModel(Operation operation)
        {
            this.Title = operation.Title;
            this.Url = operation.Link;
            this.FileSize = FormatString.FormatFileSize(operation.FileSize);
            this.Duration = FormatString.FormatVideoLength(operation.Duration);
            this.CancelDownloadCommand = new RelayCommand((o) => this.CancelDownloadAction?.Invoke(this), nameof(this.CancelDownloadCommand), (o) => this.Operation.CanStop());
            this.PauseDownloadCommand = new RelayCommand((o) => this.PauseDownloadAction?.Invoke(this), nameof(this.PauseDownloadCommand), (o) => this.Operation.CanPause() || this.Operation.CanResume());
            this.PlayMediaCommand = new RelayCommand(this.OnPlayMedia, nameof(this.PlayMediaCommand));
            this.ShowMediaInFolderCommand = new RelayCommand(this.OnShowMediaInFolder, nameof(this.ShowMediaInFolderCommand));

            this.Operation = operation;
            this.Operation.Completed += OnOperation_Completed;
            this.Operation.ProgressChanged += OnOperation_ProgressChanged;
            this.Operation.PropertyChanged += OnOperation_PropertyChanged;
            this.Operation.ReportsProgressChanged += OnOperation_ReportsProgressChanged;
            this.Operation.Started += OnOperation_Started;
            this.Operation.StatusChanged += OnOperation_StatusChanged;

            // Set Status text, so it's not empty until a StatusChanged event is fired
            this.OnOperation_StatusChanged(this, EventArgs.Empty);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the OperationComplete.
        /// </summary>
        public event OperationEventHandler OperationComplete;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the CancelDownloadCommand.
        /// </summary>
        public ICommand CancelDownloadCommand { get; internal set; }

        /// <summary>
        /// Gets the Duration.
        /// </summary>
        public string Duration { get => this._duration; private set => this.Set(this.PropertyChangedHandler, ref this._duration, value); }

        /// <summary>
        /// Gets the FileSize.
        /// </summary>
        public string FileSize { get => this._fileSize; private set => this.Set(this.PropertyChangedHandler, ref this._fileSize, value); }

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
        /// Gets the Operation.
        /// </summary>
        public Operation Operation { get; private set; }

        /// <summary>
        /// Gets the PauseDownloadCommand.
        /// </summary>
        public ICommand PauseDownloadCommand { get; }

        /// <summary>
        /// Gets or sets the PauseText.
        /// </summary>
        public string PauseText { get => _pauseText; internal set => this.Set(this.PropertyChangedHandler, ref _pauseText, value); }

        /// <summary>
        /// Gets the PlayMediaCommand.
        /// </summary>
        public ICommand PlayMediaCommand { get; }

        /// <summary>
        /// Gets the Progress.
        /// </summary>
        public int Progress { get => this._progress; private set => this.Set(this.PropertyChangedHandler, ref this._progress, value); }

        /// <summary>
        /// Gets the ProgressText.
        /// </summary>
        public string ProgressText
        {
            get
            {
                string status = string.Empty;

                switch (this.Operation.Status)
                {
                    case OperationStatus.Working:
                        status = $"{this.Operation.ProgressPercentage}%";

                        if (!string.IsNullOrEmpty(this.Status))
                            status += $" ({this.Status})";
                        break;

                    default:
                        status = this.Status;
                        break;
                }

                return status;
            }
        }

        /// <summary>
        /// Gets the ShowMediaInFolderCommand.
        /// </summary>
        public ICommand ShowMediaInFolderCommand { get; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string Status { get => this._status; set => this.Set(this.PropertyChangedHandler, ref this._status, value); }

        /// <summary>
        /// Gets the Stopwatch.
        /// </summary>
        public Stopwatch Stopwatch { get; private set; }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get => this._title; private set => this.Set(this.PropertyChangedHandler, ref this._title, value); }

        /// <summary>
        /// Gets the Url.
        /// </summary>
        public string Url { get => this._url; private set => this.Set(this.PropertyChangedHandler, ref this._url, value); }

        /// <summary>
        /// Gets or sets the CancelDownloadAction.
        /// </summary>
        internal Action<OperationModel> CancelDownloadAction { get; set; }

        /// <summary>
        /// Gets or sets the PauseDownloadAction.
        /// </summary>
        internal Action<OperationModel> PauseDownloadAction { get; set; }

        /// <summary>
        /// Gets the ProgressMaximum.
        /// </summary>
        private static int ProgressMaximum { get; } = 100;

        /// <summary>
        /// Gets the ProgressMinimum.
        /// </summary>
        private static int ProgressMinimum { get; } = 0;

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.Operation.CanStop())
            {
                this.Operation.Stop();
            }

            this.Operation.Dispose();
            this.CancelDownloadAction = null;
            this.PauseDownloadAction = null;
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="OperationModel"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(OperationModel other)
        {
            var isEqual = other != null && this.Operation.Output == other.Operation.Output;
            return isEqual;
        }

        /// <summary>
        /// The OnOperation_Completed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="OperationEventArgs"/>.</param>
        private void OnOperation_Completed(object sender, OperationEventArgs e)
        {
            this.Stopwatch?.Stop();
            this.Stopwatch = null;

            if (File.Exists(this.Operation.Output))
            {
                this.FileSize = string.Format(new ByteFormatProvider(), "{0:fs}", this.Operation.Output);
            }
            else if (Directory.Exists(this.Operation.Output))
            {
                /* Get total file size of all affected files
                 *
                 * Directory can contain unrelated files, so make use of List properties
                 * from Operation that contains the affected files only.
                 */
                string[] fileList = null;

                ////if (this.Operation is BatchOperation)
                ////    fileList = (this.Operation as BatchOperation).DownloadedFiles.ToArray();
                ////else if (this.Operation is ConvertOperation)
                ////    fileList = (this.Operation as ConvertOperation).ProcessedFiles.ToArray();
                ////else if (this.Operation is PlaylistOperation)
                ////    fileList = (this.Operation as PlaylistOperation).DownloadedFiles.ToArray();
                ////else
                ////    throw new Exception("Couldn't get affected file list from operation " + this.Operation.GetType().Name);

                long fileSize = fileList.Sum(f => FileHelper.GetFileSize(f));

                this.FileSize = FormatString.FormatFileSize(fileSize);
            }

            this.Progress = ProgressMaximum;
            this.OperationComplete?.Invoke(this, e);
        }

        /// <summary>
        /// The OnOperation_ProgressChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/>.</param>
        private void OnOperation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Progress = Math.Min(ProgressMaximum, Math.Max(ProgressMinimum, e.ProgressPercentage));
            if (!string.IsNullOrEmpty(this.Operation.ProgressText))
            {
                this.Status = this.Operation.ProgressText;
            }
            else
            {
                if (this.Wait())
                {
                    return;
                }

                this.Stopwatch?.Restart();
                this.Status = this.Operation.Speed + this.Operation.ETA;
            }
        }

        /// <summary>
        /// The OnOperation_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnOperation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Operation.Duration):
                    this.Duration = FormatString.FormatVideoLength(this.Operation.Duration);
                    break;

                case nameof(Operation.FileSize):
                    this.FileSize = FormatString.FormatFileSize(this.Operation.FileSize);
                    break;

                case nameof(Operation.Input):
                    this.Url = this.Operation.Input;
                    break;

                case nameof(Operation.Title):
                    this.Title = this.Operation.Title;
                    break;
            }
        }

        /// <summary>
        /// The OnOperation_ReportsProgressChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void OnOperation_ReportsProgressChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The OnOperation_Started.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void OnOperation_Started(object sender, EventArgs e)
        {
            this.Stopwatch = new Stopwatch();
            this.Stopwatch.Start();
        }

        /// <summary>
        /// The OnOperation_StatusChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void OnOperation_StatusChanged(object sender, EventArgs e)
        {
            switch (this.Operation.Status)
            {
                case OperationStatus.Success:
                    this.Status = "Completed";
                    this.IsQueuedControlsVisible = false;
                    break;

                case OperationStatus.Canceled:
                case OperationStatus.Failed:
                case OperationStatus.Paused:
                case OperationStatus.Queued:
                    this.Status = this.Operation.Status.ToString();
                    break;

                case OperationStatus.Working:
                    if (!string.IsNullOrEmpty(this.Operation.ProgressText))
                    {
                        this.Status = this.Operation.ProgressText;
                    }

                    break;
            }
        }

        /// <summary>
        /// The OnPlayMedia.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPlayMedia(object obj)
        {
            Process.Start(this.Operation.Output);
        }

        /// <summary>
        /// The OnShowMediaInFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnShowMediaInFolder(object obj)
        {
            var path = Path.GetDirectoryName(this.Operation.Output);
            Process.Start(path);
        }

        /// <summary>
        /// The Wait.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool Wait()
        {
            // Limit the progress update to avoid flickering.
            if (this.Stopwatch == null || !this.Stopwatch.IsRunning)
            {
                return false;
            }

            return this.Stopwatch.ElapsedMilliseconds < AppEnvironment.ProgressUpdateDelay;
        }

        #endregion Methods
    }
}