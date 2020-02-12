namespace YoutubeDlGui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="VideoBrowserViewModel" />
    /// </summary>
    public class VideoBrowserViewModel : NotifyPropertyChanged
    {
        #region Fields

        private readonly Dictionary<string, string> _cookies;

        private ICommand _backwardCommand;

        private bool _canBackward;

        private bool _canForward;

        private ICommand _forwardCommand;

        private bool _isDownloadable;

        private string _navigateUrl = "youtube.com";

        private ICommand _reloadCommand;

        private string _webUri;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoBrowserViewModel"/> class.
        /// </summary>
        internal VideoBrowserViewModel()
        {
            // BackwardCommand and ForwardCommand are set by the View.
            this.DownloadCommand = new RelayCommand(this.OnDownload, (o) => this.UrlReader.UrlHandler.IsDownloadable);
            this.HomeCommand = new RelayCommand(this.OnHome);
            this.NavigateUrlCommand = new RelayCommand(this.OnNavigateUrl);
            this.SettingsCommand = new RelayCommand(this.OnSettings);
            _cookies = new Dictionary<string, string>();
            IndicatorColor = new SolidColorBrush(Colors.DarkBlue);
            this.Url = "http://www.youtube.com";
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackwardCommand
        /// </summary>
        public ICommand BackwardCommand { get => this._backwardCommand; set => this.Set(this.PropertyChangedHandler, ref this._backwardCommand, value); }

        /// <summary>
        /// Gets or sets a value indicating whether CanBackward
        /// Gets or sets the CanBackward.
        /// </summary>
        public bool CanBackward { get => this._canBackward; set => this.Set(this.PropertyChangedHandler, ref this._canBackward, value); }

        /// <summary>
        /// Gets or sets a value indicating whether CanForward
        /// Gets or sets the CanForward.
        /// </summary>
        public bool CanForward { get => this._canForward; set => this.Set(this.PropertyChangedHandler, ref this._canForward, value); }

        /// <summary>
        /// Gets the DownloadCommand
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets or sets the ForwardCommand
        /// </summary>
        public ICommand ForwardCommand { get => this._forwardCommand; set => this.Set(this.PropertyChangedHandler, ref this._forwardCommand, value); }

        /// <summary>
        /// Gets the HomeCommand
        /// </summary>
        public ICommand HomeCommand { get; }

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon { get; set; } = Icons.SearchVideo;

        /// <summary>
        /// Gets the IndicatorColor
        /// </summary>
        public Brush IndicatorColor { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDownloadable
        /// Gets or sets the IsDownloadable.
        /// </summary>
        public bool IsDownloadable { get => this._isDownloadable; set => this.Set(this.PropertyChangedHandler, ref this._isDownloadable, value); }

        /// <summary>
        /// Sets the IsSuccessful
        /// </summary>
        public bool? IsSuccessful
        {
            set
            {
                if (value == null)
                    IndicatorColor = new SolidColorBrush(Colors.LightBlue);
                else if (value == false)
                    IndicatorColor = new SolidColorBrush(Colors.Red);
                else
                    IndicatorColor = new SolidColorBrush(Colors.Green);
                OnPropertyChanged(nameof(IndicatorColor));
            }
        }

        /// <summary>
        /// Gets or sets the NavigateUrl.
        /// The current valid Url that is currently opened.
        /// It is set by Url property if the Return key is pressed or link is clicked.
        /// </summary>
        public string NavigateUrl { get => this._navigateUrl; set => this.Set(this.PropertyChangedHandler, ref this._navigateUrl, value); }

        /// <summary>
        /// Gets the NavigateUrlCommand
        /// </summary>
        public ICommand NavigateUrlCommand { get; }

        /// <summary>
        /// Gets or sets the ReloadCommand
        /// </summary>
        public ICommand ReloadCommand { get => this._reloadCommand; set => this.Set(this.PropertyChangedHandler, ref this._reloadCommand, value); }

        /// <summary>
        /// Gets the SettingsCommand
        /// </summary>
        public ICommand SettingsCommand { get; }

        /// <summary>
        /// Gets or sets the WebUri that is typed in the TextBox.
        /// Gets the WebUri
        /// </summary>
        public string Url { get => this._webUri; set => this.Set(this.PropertyChangedHandler, ref this._webUri, value); }

        /// <summary>
        /// Gets the UrlReader
        /// </summary>
        public UrlReader UrlReader { get; } = new UrlReader();

        /// <summary>
        /// Gets the WebCookies
        /// </summary>
        public string WebCookies => string.Join("; ", _cookies.Select(x => $"{x.Key}={x.Value}"));

        /// <summary>
        /// Gets or sets the DownloadAction
        /// </summary>
        internal Action<string> DownloadAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The IsUrlValid
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool IsUrlValid()
        {
            return true;
        }

        /// <summary>
        /// The OnDownload
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnDownload(object obj)
        {
            if (!this.IsUrlValid())
            {
                return;
            }

            this.DownloadAction(this.Url);
        }

        /// <summary>
        /// The OnHome
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnHome(object obj)
        {
        }

        /// <summary>
        /// The OnNavigateUrl
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnNavigateUrl(object obj)
        {
            this.NavigateUrl = this.Url;
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/></param>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Url):
                    this.UrlReader.Url = this.Url;
                    break;

                case nameof(this.NavigateUrl):
                    this.Url = this.NavigateUrl;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// The OnSettings
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnSettings(object obj)
        {
        }

        #endregion Methods
    }
}