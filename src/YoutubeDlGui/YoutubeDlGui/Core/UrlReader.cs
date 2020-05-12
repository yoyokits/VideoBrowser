namespace YoutubeDlGui.Core
{
    using System;
    using System.ComponentModel;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.If;

    /// <summary>
    /// Defines the <see cref="UrlReader" />
    /// </summary>
    public class UrlReader : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private string _url;

        private IUrlHandler _urlHandler;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlReader"/> class.
        /// </summary>
        internal UrlReader()
        {
            this.UrlHandlerProvider = new UrlHandlerProvider();
            this.UrlHandler = UrlHandlerProvider.NoneUrlHandler;
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
        /// Gets or sets the Url
        /// </summary>
        public string Url { get => this._url; set => this.Set(this.PropertyChanged, ref this._url, value); }

        /// <summary>
        /// Gets the UrlHandler
        /// Gets or sets the UrlHandler
        /// </summary>
        public IUrlHandler UrlHandler
        {
            get => this._urlHandler;
            private set
            {
                if (value == null)
                {
                    return;
                }

                this.Set(this.PropertyChanged, ref this._urlHandler, value);
            }
        }

        /// <summary>
        /// Gets the UrlHandlerProvider
        /// </summary>
        public UrlHandlerProvider UrlHandlerProvider { get; }

        /// <summary>
        /// Gets a value indicating whether IsDownloadable
        /// </summary>
        internal bool IsDownloadable => this.UrlHandler != null ? this.UrlHandler.IsDownloadable : false;

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
            this.PropertyChanged -= this.OnPropertyChanged;
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/></param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Url):
                    this.UrlHandler = this.UrlHandlerProvider.GetUrlHandler(this.Url);
                    this.UrlHandler.FullUrl = this.Url;
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}