namespace YoutubeDlGui.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewModel" />
    /// </summary>
    public class UrlEditorViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _fileName;

        private string _imageUrl;

        private string _outputFolder;

        private string _url;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlEditorViewModel"/> class.
        /// </summary>
        internal UrlEditorViewModel()
        {
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

        #endregion Properties

        #region Methods

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

        #endregion Methods
    }
}