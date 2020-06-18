namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using CefSharp;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="VideoBrowserViewModel" />.
    /// </summary>
    public class VideoBrowserViewModel : NotifyPropertyChanged, IDisposable
    {
        #region Fields

        private ICommand _backwardCommand;

        private bool _canBackward;

        private bool _canForward;

        private CefWindowData _cefWindowData;

        private ICommand _forwardCommand;

        private string _header = "New Tab";

        private string _navigateUrl = "youtube.com";

        private ICommand _reloadCommand;

        private IWebBrowser _webBrowser;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoBrowserViewModel"/> class.
        /// </summary>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        /// <param name="windowData">The windowData<see cref="CefWindowData"/>.</param>
        internal VideoBrowserViewModel(GlobalBrowserData globalBrowserData, CefWindowData windowData)
        {
            this.GlobalBrowserData = globalBrowserData;
            this.CefWindowData = windowData;

            // BackwardCommand and ForwardCommand are set by the View.
            this.DownloadCommand = new RelayCommand(this.OnDownload, "Download", (o) => this.UrlReader.IsDownloadable);
            this.HomeCommand = new RelayCommand(this.OnHome, "Home");
            this.NavigateUrlCommand = new RelayCommand(this.OnNavigateUrl, "NavigateUrl");
            this.OpenOutputFolderCommand = new RelayCommand(this.OnOpenOutputFolder, "Open output folder");
            this.IndicatorColor = new SolidColorBrush(Colors.DarkBlue);
            this.UrlEditor = new UrlEditorViewModel(this.UrlReader, this.GlobalBrowserData.Settings)
            {
                NavigateUrlCommand = this.NavigateUrlCommand,
                ShowMessageAsyncAction = this.ShowMessageAsync
            };
            this.UrlEditor.PropertyChanged += this.OnUrlEditor_PropertyChanged;
            this.PropertyChanged += this.OnPropertyChanged;
            this.OnHome(null);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddInButtons.
        /// </summary>
        public ICollection<AddInButton> AddInButtons => this.GlobalBrowserData.AddInButtons;

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
        /// Gets or sets the CefWindowData.
        /// </summary>
        public CefWindowData CefWindowData
        {
            get => _cefWindowData;
            internal set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _cefWindowData, value))
                {
                    return;
                };

                this.InitializeHandlers();
            }
        }

        /// <summary>
        /// Gets the DownloadCommand.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Gets or sets the ForwardCommand.
        /// </summary>
        public ICommand ForwardCommand { get => this._forwardCommand; set => this.Set(this.PropertyChangedHandler, ref this._forwardCommand, value); }

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        public GlobalBrowserData GlobalBrowserData { get; }

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
        /// The current valid Url that is currently opened,
        /// it is set by Url property if the Return key is pressed or link is clicked...
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
        /// Gets or sets the Url.
        /// It is typed in the TextBox.
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
        /// Gets or sets the WebBrowser.
        /// </summary>
        public IWebBrowser WebBrowser
        {
            get => _webBrowser;
            set
            {
                if (this.WebBrowser == value)
                {
                    return;
                }

                _webBrowser = value;
                this.InitializeHandlers();
            }
        }

        /// <summary>
        /// Gets or sets the DownloadAction.
        /// </summary>
        internal Action<Operation> DownloadAction { get => this.UrlEditor.DownloadAction; set => this.UrlEditor.DownloadAction = value; }

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
        /// The InitializeHandlers.
        /// </summary>
        private void InitializeHandlers()
        {
            if (this.WebBrowser != null)
            {
                this.WebBrowser.RequestHandler = this.CefWindowData.CefRequestHandler;
                this.WebBrowser.MenuHandler = this.CefWindowData.CefContextMenuHandler;
            }
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
            this.CefWindowData.IsAirspaceVisible = false;
        }

        /// <summary>
        /// The OnOpenOutputFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnOpenOutputFolder(object obj)
        {
            Process.Start(this.GlobalBrowserData.Settings.OutputFolder);
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

        /// <summary>
        /// The ShowMessage.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        private void ShowMessageAsync(string title, string message)
        {
            this.CefWindowData.ShowMessageAsync(title, message);
        }

        #endregion Methods
    }
}