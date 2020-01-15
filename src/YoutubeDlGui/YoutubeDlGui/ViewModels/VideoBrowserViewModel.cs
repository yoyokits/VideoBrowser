namespace YoutubeDlGui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="VideoBrowserViewModel" />
    /// </summary>
    public class VideoBrowserViewModel : NotifyPropertyChanged
    {
        #region Fields

        private readonly Dictionary<string, string> _cookies;

        private bool _isDownloadable;

        private string _webUri;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoBrowserViewModel"/> class.
        /// </summary>
        internal VideoBrowserViewModel()
        {
            this.DownloadCommand = new RelayCommand(this.OnDownload);
            this.ForwardCommand = new RelayCommand(this.OnForward);
            this.BackCommand = new RelayCommand(this.OnBack);
            this.SettingsCommand = new RelayCommand(this.OnSettings);
            this.HomeCommand = new RelayCommand(this.OnHome);
            _cookies = new Dictionary<string, string>();
            IndicatorColor = new SolidColorBrush(Colors.DarkBlue);
            this.WebUri = "http://www.youtube.com";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BackCommand
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Gets the DownloadCommand
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets the ForwardCommand
        /// </summary>
        public ICommand ForwardCommand { get; }

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
        /// Gets the SettingsCommand
        /// </summary>
        public ICommand SettingsCommand { get; }

        /// <summary>
        /// Gets the WebCookies
        /// </summary>
        public string WebCookies => string.Join("; ", _cookies.Select(x => $"{x.Key}={x.Value}"));

        /// <summary>
        /// Gets or sets the WebUri
        /// Gets the WebUri
        /// </summary>
        public string WebUri { get => this._webUri; set => this.Set(this.PropertyChangedHandler, ref this._webUri, value); }

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
        /// The OnBack
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnBack(object obj)
        {
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

            this.DownloadAction(this.WebUri);
        }

        /// <summary>
        /// The OnForward
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnForward(object obj)
        {
        }

        /// <summary>
        /// The OnHome
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnHome(object obj)
        {
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