namespace YoutubeDlGui.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Helpers;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewModel" />
    /// </summary>
    public class UrlEditorViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private string _duration;

        private string _fileName;

        private string _imageUrl;

        private bool _isVisible;

        private string _navigateUrl;

        private string _outputFolder;

        private string _url;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlEditorViewModel"/> class.
        /// </summary>
        /// <param name="reader">The reader<see cref="UrlReader"/></param>
        internal UrlEditorViewModel(UrlReader reader)
        {
            this.UrlReader = reader;
            this.UrlReader.PropertyChanged += this.OnUrlReader_PropertyChanged;
            this.DownloadCommand = new RelayCommand(this.OnDownload);
            this.GetFolderCommand = new RelayCommand(this.GetFolder);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the DownloadCommand
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets the Duration
        /// Gets or sets the Duration.
        /// </summary>
        public string Duration { get => this._duration; private set => this.Set(this.PropertyChanged, ref this._duration, value); }

        /// <summary>
        /// Gets or sets the FileName
        /// </summary>
        public string FileName { get => this._fileName; set => this.Set(this.PropertyChanged, ref this._fileName, value); }

        /// <summary>
        /// Gets the GetFolderCommand
        /// </summary>
        public ICommand GetFolderCommand { get; }

        /// <summary>
        /// Gets or sets the ImageUrl
        /// </summary>
        public string ImageUrl { get => this._imageUrl; set => this.Set(this.PropertyChanged, ref this._imageUrl, value); }

        /// <summary>
        /// Gets a value indicating whether IsVisible
        /// Gets or sets the IsVisible.
        /// </summary>
        public bool IsVisible { get => this._isVisible; private set => this.Set(this.PropertyChanged, ref this._isVisible, value); }

        /// <summary>
        /// Gets or sets the NavigateUrl
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
                    Task.Run(() =>
                    {
                        var info = YoutubeDl.GetVideoInfo(this.NavigateUrl);
                        this.FileName = info.Title;
                        this.Duration = info.Duration.FormatVideoLength();
                        this.ImageUrl = info.ThumbnailUrl;
                    });
                }

                this.IsVisible = this.UrlReader.IsDownloadable;
            }
        }

        /// <summary>
        /// Gets or sets the NavigateUrlCommand
        /// </summary>
        public ICommand NavigateUrlCommand { get; internal set; }

        /// <summary>
        /// Gets or sets the OutputFolder
        /// </summary>
        public string OutputFolder { get => this._outputFolder; set => this.Set(this.PropertyChanged, ref this._outputFolder, value); }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get => this._url; set => this.Set(this.PropertyChanged, ref this._url, value); }

        /// <summary>
        /// Gets the UrlReader
        /// Gets or sets the UrlReader
        /// </summary>
        public UrlReader UrlReader { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
            this.UrlReader.PropertyChanged -= this.OnUrlReader_PropertyChanged;
        }

        /// <summary>
        /// The GetFolder
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void GetFolder(object obj)
        {
        }

        /// <summary>
        /// The OnDownload
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnDownload(object obj)
        {
        }

        /// <summary>
        /// The OnUrlReader_PropertyChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/></param>
        private void OnUrlReader_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.UrlReader.IsDownloadable):
                    break;
            }
        }

        #endregion Methods
    }
}