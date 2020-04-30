namespace YoutubeDlGui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Helpers;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Properties;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewModel" />.
    /// </summary>
    public class UrlEditorViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private string _duration;

        private string _fileName;

        private string _fileSize;

        private IList<VideoFormat> _formats;

        private string _imageUrl;

        private bool _isBusy;

        private bool _isDownloadable;

        private bool _isFormatComboBoxVisible;

        private bool _isVisible;

        private string _navigateUrl;

        private string _outputFolder;

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
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal UrlEditorViewModel(UrlReader reader, GlobalData globalData)
        {
            this.UrlReader = reader;
            this.GlobalData = globalData;
            this.OutputFolder = string.IsNullOrEmpty(Settings.Default.DownloadFolder) ? AppEnvironment.UserVideoFolder : Settings.Default.DownloadFolder;
            this.UrlReader.PropertyChanged += this.OnUrlReader_PropertyChanged;
            this.DownloadCommand = new RelayCommand(this.OnDownload);
            this.GetFolderCommand = new RelayCommand(this.GetFolder);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the DownloadCommand.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets the Duration.
        /// </summary>
        public string Duration { get => this._duration; private set => this.Set(this.PropertyChanged, ref this._duration, value); }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        public string FileName { get => this._fileName; set => this.Set(this.PropertyChanged, ref this._fileName, value); }

        /// <summary>
        /// Gets the FileSize.
        /// </summary>
        public string FileSize { get => _fileSize; private set => this.Set(this.PropertyChanged, ref _fileSize, value); }

        /// <summary>
        /// Gets or sets the Formats.
        /// </summary>
        public IList<VideoFormat> Formats { get => this._formats; set => this.Set(this.PropertyChanged, ref this._formats, value); }

        /// <summary>
        /// Gets the GetFolderCommand.
        /// </summary>
        public ICommand GetFolderCommand { get; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the ImageUrl.
        /// </summary>
        public string ImageUrl { get => this._imageUrl; set => this.Set(this.PropertyChanged, ref this._imageUrl, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy.
        /// </summary>
        public bool IsBusy { get => _isBusy; set => this.Set(this.PropertyChanged, ref _isBusy, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsDownloadable.
        /// </summary>
        public bool IsDownloadable { get => _isDownloadable; set => this.Set(this.PropertyChanged, ref _isDownloadable, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsFormatComboBoxVisible.
        /// </summary>
        public bool IsFormatComboBoxVisible { get => _isFormatComboBoxVisible; set => this.Set(this.PropertyChanged, ref _isFormatComboBoxVisible, value); }

        /// <summary>
        /// Gets a value indicating whether IsVisible.
        /// </summary>
        public bool IsVisible { get => this._isVisible; private set => this.Set(this.PropertyChanged, ref this._isVisible, value); }

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
        /// Gets or sets the OutputFolder.
        /// </summary>
        public string OutputFolder
        {
            get => this._outputFolder;
            set
            {
                this.Set(this.PropertyChanged, ref this._outputFolder, value);
                if (Directory.Exists(this.OutputFolder))
                {
                    Settings.Default.DownloadFolder = this.OutputFolder;
                }
            }
        }

        /// <summary>
        /// Gets or sets the SelectedFormat.
        /// </summary>
        public VideoFormat SelectedFormat { get => this._selectedFormat; set => this.Set(this.PropertyChanged, ref this._selectedFormat, value); }

        /// <summary>
        /// Gets or sets the SelectedFormatIndex.
        /// </summary>
        public int SelectedFormatIndex { get => _selectedFormatIndex; set => this.Set(this.PropertyChanged, ref _selectedFormatIndex, value); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => this._url; set => this.Set(this.PropertyChanged, ref this._url, value); }

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

                this.Set(this.PropertyChanged, ref this._videoInfo, value);
            }
        }

        /// <summary>
        /// Gets or sets the DownloadAction.
        /// </summary>
        internal Action<Operation> DownloadAction { get; set; }

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
        /// The GetFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void GetFolder(object obj)
        {
        }

        /// <summary>
        /// The LoadVideoInfo.
        /// </summary>
        /// <param name="task">The task<see cref="Task"/>.</param>
        private void LoadVideoInfo(Task task)
        {
            this.IsBusy = false;
            this.IsDownloadable = false;
            this.IsFormatComboBoxVisible = false;

            if (this.VideoInfo.RequiresAuthentication)
            {
                ////var auth = Dialogs.LoginDialog.Show(this);
            }
            else if (this.VideoInfo.Failure)
            {
                MessageBox.Show("Couldn't retrieve video. Reason:\n\n" + this.VideoInfo.FailureReason, "Error Loading Video Info");
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
            Settings.Default.LastUrl = this.NavigateUrl;
        }

        /// <summary>
        /// The OnDownload.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        private void OnDownload(object o)
        {
            var format = this.SelectedFormat;
            this.FileName = FileHelper.GetValidFilename(this.FileName);
            var fileName = $"{this.FileName}.{format.Extension}";
            var output = Path.Combine(this.OutputFolder, fileName);
            if (File.Exists(output))
            {
                var message = $@"File ""{fileName}"" is already downloaded";
                this.GlobalData.ShowMessageAsync("Download Canceled", message);
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

        #endregion Methods
    }
}