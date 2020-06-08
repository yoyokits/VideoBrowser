namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="VideoBrowserViewModel" />.
    /// </summary>
    public class VideoBrowserViewModel : NotifyPropertyChanged, IDisposable
    {
        #region Fields

        private readonly Dictionary<string, string> _cookies;

        private ICommand _backwardCommand;

        private bool _canBackward;

        private bool _canForward;

        private ICommand _forwardCommand;

        private string _header = "Cekli";

        private string _navigateUrl = "youtube.com";

        private ICommand _reloadCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoBrowserViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal VideoBrowserViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;

            // BackwardCommand and ForwardCommand are set by the View.
            this.DownloadCommand = new RelayCommand(this.OnDownload, "Download", (o) => this.UrlReader.IsDownloadable);
            this.HomeCommand = new RelayCommand(this.OnHome, "Home");
            this.NavigateUrlCommand = new RelayCommand(this.OnNavigateUrl, "NavigateUrl");
            this.OpenOutputFolderCommand = new RelayCommand(this.OnOpenOutputFolder, "Open output folder");
            this._cookies = new Dictionary<string, string>();
            this.IndicatorColor = new SolidColorBrush(Colors.DarkBlue);
            this.UrlEditor = new UrlEditorViewModel(this.UrlReader, globalData)
            {
                NavigateUrlCommand = this.NavigateUrlCommand,
                DownloadAction = globalData.DownloadQueueViewModel.Download
            };
            this.UrlEditor.PropertyChanged += this.OnUrlEditor_PropertyChanged;
            this.PropertyChanged += this.OnPropertyChanged;
            this.OnHome(null);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackwardCommand.
        /// </summary>
        public ICommand BackwardCommand { get => this._backwardCommand; set => this.Set(this.PropertyChangedHandler, ref this._backwardCommand, value); }

        /// <summary>
        /// Gets or sets a value indicating whether CanBackward.
        /// </summary>
        public bool CanBackward { get => this._canBackward; set => this.Set(this.PropertyChangedHandler, ref this._canBackward, value); }

        /// <summary>
        /// Gets or sets a value indicating whether CanForward.
        /// </summary>
        public bool CanForward { get => this._canForward; set => this.Set(this.PropertyChangedHandler, ref this._canForward, value); }

        /// <summary>
        /// Gets the DownloadCommand.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets or sets the ForwardCommand.
        /// </summary>
        public ICommand ForwardCommand { get => this._forwardCommand; set => this.Set(this.PropertyChangedHandler, ref this._forwardCommand, value); }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        public string Header { get => _header; set => this.Set(this.PropertyChangedHandler, ref _header, value); }

        /// <summary>
        /// Gets the HomeCommand.
        /// </summary>
        public ICommand HomeCommand { get; }

        /// <summary>
        /// Gets the IndicatorColor.
        /// </summary>
        public Brush IndicatorColor { get; private set; }

        /// <summary>
        /// Sets the IsSuccessful.
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
        /// Gets the NavigateUrlCommand.
        /// </summary>
        public ICommand NavigateUrlCommand { get; }

        /// <summary>
        /// Gets the OpenOutputFolderCommand.
        /// </summary>
        public ICommand OpenOutputFolderCommand { get; }

        /// <summary>
        /// Gets or sets the ReloadCommand.
        /// </summary>
        public ICommand ReloadCommand { get => this._reloadCommand; set => this.Set(this.PropertyChangedHandler, ref this._reloadCommand, value); }

        /// <summary>
        /// Gets or sets the WebUri that is typed in the TextBox.
        /// </summary>
        public string Url { get => this.UrlEditor.Url; set => this.UrlEditor.Url = value; }

        /// <summary>
        /// Gets the UrlEditor.
        /// </summary>
        public UrlEditorViewModel UrlEditor { get; }

        /// <summary>
        /// Gets the UrlReader.
        /// </summary>
        public UrlReader UrlReader { get; } = new UrlReader();

        /// <summary>
        /// Gets the WebCookies.
        /// </summary>
        public string WebCookies => string.Join("; ", _cookies.Select(x => $"{x.Key}={x.Value}"));

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.UrlEditor.Dispose();
            this.UrlEditor.PropertyChanged -= this.OnUrlEditor_PropertyChanged;
            this.UrlReader.Dispose();
        }

        /// <summary>
        /// The IsUrlValid.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool IsUrlValid()
        {
            return true;
        }

        /// <summary>
        /// The OnDownload.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnDownload(object obj)
        {
            if (!this.IsUrlValid() || !this.UrlEditor.DownloadCommand.CanExecute(null))
            {
                return;
            }

            this.UrlEditor.DownloadCommand.Execute(null);
        }

        /// <summary>
        /// The OnHome.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnHome(object obj)
        {
            this.Url = AppEnvironment.HomeUrl;
            this.NavigateUrl = this.Url;
        }

        /// <summary>
        /// The OnNavigateUrl called from Button.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnNavigateUrl(object obj)
        {
            this.Url = UrlHelper.GetValidUrl(this.Url);
            this.NavigateUrl = this.Url;
            this.GlobalData.IsAirspaceVisible = false;
        }

        /// <summary>
        /// The OnOpenOutputFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnOpenOutputFolder(object obj)
        {
            Process.Start(this.GlobalData.Settings.OutputFolder);
        }

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Url):
                    this.UrlReader.Url = this.Url;
                    break;

                case nameof(this.NavigateUrl):
                    this.Url = this.NavigateUrl;
                    this.UrlEditor.NavigateUrl = this.NavigateUrl;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// The OnUrlEditor_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnUrlEditor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsMatch(nameof(this.UrlEditor.Url)))
            {
                this.OnPropertyChanged(nameof(this.Url));
            }
        }

        #endregion Methods
    }
}