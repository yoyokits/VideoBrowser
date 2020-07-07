namespace VideoBrowser.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewModel" />.
    /// </summary>
    public class UrlEditorViewModel : NotifyPropertyChanged, IDisposable
    {
        #region Fields

        private string _duration;

        private string _fileName;

        private string _fileSize;

        private IList<VideoFormat> _formats;

        private string _imageUrl;

        private bool _isBusy;

        private bool _isDownloadable;

        private bool _isFocused;

        private bool _isFormatComboBoxVisible;

        private bool _isTextBoxFocused;

        private bool _isVisible;

        private string _navigateUrl;

        private VideoFormat _selectedFormat;

        private int _selectedFormatIndex;

        private string _url;

        private VideoInfo _videoInfo;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlEditorViewModel"/> class.
        /// </summary>
        /// <param name="reader">The reader<see cref="UrlReader"/>.</param>
        /// <param name="settings">The settings<see cref="Settings"/>.</param>
        internal UrlEditorViewModel(UrlReader reader, SettingsViewModel settings)
        {
            this.UrlReader = reader;
            this.UrlReader.PropertyChanged += this.OnUrlReader_PropertyChanged;
            this.Settings = settings;
            this.DownloadCommand = new RelayCommand(this.OnDownload);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadCommand.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public string Duration { get => this._duration; internal set => this.Set(this.PropertyChangedHandler, ref this._duration, value); }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        public string FileName { get => this._fileName; internal set => this.Set(this.PropertyChangedHandler, ref this._fileName, value); }

        /// <summary>
        /// Gets or sets the FileSize.
        /// </summary>
        public string FileSize { get => _fileSize; internal set => this.Set(this.PropertyChangedHandler, ref _fileSize, value); }

        /// <summary>
        /// Gets or sets the Formats.
        /// </summary>
        public IList<VideoFormat> Formats { get => this._formats; internal set => this.Set(this.PropertyChangedHandler, ref this._formats, value); }

        /// <summary>
        /// Gets the GetFolderCommand.
        /// </summary>
        public ICommand GetFolderCommand => this.Settings.GetFolderCommand;

        /// <summary>
        /// Gets or sets the ImageUrl.
        /// </summary>
        public string ImageUrl { get => this._imageUrl; set => this.Set(this.PropertyChangedHandler, ref this._imageUrl, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy.
        /// </summary>
        public bool IsBusy { get => _isBusy; set => this.Set(this.PropertyChangedHandler, ref _isBusy, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsDownloadable.
        /// </summary>
        public bool IsDownloadable { get => _isDownloadable; set => this.Set(this.PropertyChangedHandler, ref _isDownloadable, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsFocused.
        /// </summary>
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (_isFocused == value)
                {
                    return;
                }

                _isFocused = value;
                this.InvokePropertiesChanged(this.OnPropertyChanged, nameof(this.IsFocused), nameof(this.IsVisible));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsFormatComboBoxVisible.
        /// </summary>
        public bool IsFormatComboBoxVisible { get => _isFormatComboBoxVisible; set => this.Set(this.PropertyChangedHandler, ref _isFormatComboBoxVisible, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsTextBoxFocused.
        /// </summary>
        public bool IsTextBoxFocused { get => _isTextBoxFocused; set => this.Set(this.PropertyChangedHandler, ref _isTextBoxFocused, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsVisible.
        /// </summary>
        public bool IsVisible { get => this._isVisible && this.IsFocused; internal set => this.Set(this.PropertyChangedHandler, ref this._isVisible, value); }

        /// <summary>
        /// Gets or sets the NavigateUrl.
        /// </summary>
        public string NavigateUrl
        {
            get => this._navigateUrl;
            set
            {
                if (this._navigateUrl == value)
                {
                    return;
                }

                this._navigateUrl = value;
                this.IsDownloadable = false;
                if (this.UrlReader.IsDownloadable)
                {
                    this.IsBusy = true;
                    Task.Run(() =>
                    {
                        this.VideoInfo = YoutubeDl.GetVideoInfo(this.NavigateUrl);
                    }).ContinueWith(this.LoadVideoInfo);
                }

                this.IsVisible = this.UrlReader.IsDownloadable;
            }
        }

        /// <summary>
        /// Gets or sets the NavigateUrlCommand.
        /// </summary>
        public ICommand NavigateUrlCommand { get; internal set; }

        /// <summary>
        /// Gets or sets the SelectedFormat.
        /// </summary>
        public VideoFormat SelectedFormat
        {
            get => this._selectedFormat;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref this._selectedFormat, value))
                {
                    return;
                }

                this.UpdateFileSize();
            }
        }

        /// <summary>
        /// Gets or sets the SelectedFormatIndex.
        /// </summary>
        public int SelectedFormatIndex { get => _selectedFormatIndex; set => this.Set(this.PropertyChangedHandler, ref _selectedFormatIndex, value); }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings { get; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => this._url; set => this.Set(this.PropertyChangedHandler, ref this._url, value); }

        /// <summary>
        /// Gets the UrlReader.
        /// </summary>
        public UrlReader UrlReader { get; }

        /// <summary>
        /// Gets or sets the VideoInfo.
        /// </summary>
        public VideoInfo VideoInfo
        {
            get => this._videoInfo;
            set
            {
                if (this.VideoInfo != null)
                {
                    this.VideoInfo.FileSizeUpdated -= this.OnVideoInfo_FileSizeUpdated;
                }

                this.Set(this.PropertyChangedHandler, ref this._videoInfo, value);
            }
        }

        /// <summary>
        /// Gets or sets the DownloadAction.
        /// </summary>
        internal Action<Operation> DownloadAction { get; set; }

        /// <summary>
        /// Gets or sets the ShowMessageAsyncAction.
        /// </summary>
        internal Action<string, string> ShowMessageAsyncAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.UrlReader.PropertyChanged -= this.OnUrlReader_PropertyChanged;
        }

        /// <summary>
        /// The LoadVideoInfo.
        /// </summary>
        /// <param name="task">The task<see cref="Task"/>.</param>
        private void LoadVideoInfo(Task task)
        {
            this.IsBusy = false;
            this.IsFocused = true;
            this.IsTextBoxFocused = true;
            this.IsDownloadable = false;
            this.IsFormatComboBoxVisible = false;

            if (this.VideoInfo.RequiresAuthentication)
            {
                ////var auth = Dialogs.LoginDialog.Show(this);
            }
            else if (this.VideoInfo.Failure)
            {
                var message = "Couldn't retrieve video. Reason:\n\n" + this.VideoInfo.FailureReason;
                this.ShowMessageAsyncAction.Invoke("The Video Not Downloadable", message);
                Logger.Info(message);
                return;
            }
            else
            {
                this.FileName = YoutubeHelper.FormatTitle(this.VideoInfo.Title);
                this.VideoInfo.FileSizeUpdated += this.OnVideoInfo_FileSizeUpdated;
                this.Duration = this.VideoInfo.Duration.FormatVideoLength();
                this.Formats = YoutubeHelper.CheckFormats(this.VideoInfo.Formats);
                this.ImageUrl = this.VideoInfo.ThumbnailUrl;
                if (this.Formats.Any())
                {
                    this.SelectedFormatIndex = this.Formats.Count - 1;
                }
                else
                {
                    return;
                }
            }

            this.IsFormatComboBoxVisible = this.VideoInfo.Formats.Count > 0;
            this.IsDownloadable = true;
            Properties.Settings.Default.LastUrl = this.NavigateUrl;
        }

        /// <summary>
        /// The OnDownload.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        private void OnDownload(object o)
        {
            var format = this.SelectedFormat;
            var formatTitle = format.AudioOnly ? format.AudioBitRate.ToString() : format.Format.ToString();
            this.FileName = FileHelper.GetValidFilename(this.FileName);
            var fileName = $"{this.FileName}-{formatTitle}.{format.Extension}";
            var output = Path.Combine(this.Settings.OutputFolder, fileName);
            if (File.Exists(output))
            {
                var message = $@"File ""{fileName}"" is already downloaded. If it was failed, delete it and try again.";
                this.ShowMessageAsyncAction("Download Canceled", message);
                return;
            }

            DownloadOperation operation = format.AudioOnly || format.HasAudioAndVideo
                ? new DownloadOperation(format, output)
                : new DownloadOperation(format, YoutubeHelper.GetAudioFormat(format), output);
            Task.Run(() => this.DownloadAction?.Invoke(operation));
        }

        /// <summary>
        /// The OnUrlReader_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnUrlReader_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.UrlReader.IsDownloadable):
                    break;
            }
        }

        /// <summary>
        /// The OnVideoInfo_FileSizeUpdated.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FileSizeUpdateEventArgs"/>.</param>
        private void OnVideoInfo_FileSizeUpdated(object sender, FileSizeUpdateEventArgs e)
        {
            this.FileSize = FormatString.FormatFileSize(e.VideoFormat.FileSize);
        }

        /// <summary>
        /// The UpdateFileSize.
        /// </summary>
        private void UpdateFileSize()
        {
            if (this.SelectedFormat == null)
            {
                this.FileSize = "Unkown size";
            }
            else if (this.SelectedFormat.FileSize == 0)
            {
                this.FileSize = "Getting file size...";
            }
            else
            {
                var total = this.SelectedFormat.FileSize;

                // If the format is VideoOnly, combine audio and video size.
                if (this.SelectedFormat.VideoOnly)
                {
                    total += YoutubeHelper.GetAudioFormat(this.SelectedFormat).FileSize;
                }

                this.FileSize = FormatString.FormatFileSize(total);
            }
        }

        #endregion Methods
    }
}