namespace YoutubeDlGui.Core
{
    using System;
    using System.ComponentModel;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.If;

    /// <summary>
    /// Defines the <see cref="UrlHandlerBase" />
    /// </summary>
    public abstract class UrlHandlerBase : IUrlHandler, INotifyPropertyChanged
    {
        #region Fields

        private string _fullUrl;

        private bool _isDownloadable;

        private bool _isPlayList;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlHandlerBase"/> class.
        /// </summary>
        /// <param name="domainName">The domainName<see cref="string"/></param>
        protected UrlHandlerBase(string domainName)
        {
            this.DomainName = domainName;
            this.PropertyChanged += this.OnPropertyChanged;
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
        /// Gets the DomainName
        /// </summary>
        public string DomainName { get; }

        /// <summary>
        /// Gets or sets the FullUrl
        /// </summary>
        public string FullUrl { get => _fullUrl; set => this.Set(this.PropertyChanged, ref _fullUrl, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsDownloadable
        /// </summary>
        public bool IsDownloadable { get => _isDownloadable; set => this.Set(this.PropertyChanged, ref _isDownloadable, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsPlayList
        /// </summary>
        public bool IsPlayList { get => _isPlayList; set => this.Set(this.PropertyChanged, ref _isPlayList, value); }

        /// <summary>
        /// Gets the VideoUrlTypes
        /// </summary>
        public UrlTypes VideoUrlTypes => throw new NotImplementedException();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ParseFullUrl
        /// </summary>
        protected abstract void ParseFullUrl();

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/></param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.FullUrl):
                    this.ParseFullUrl();
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}