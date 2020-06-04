namespace VideoBrowser.Controls.CefSharpBrowser
{
    using CefSharp;
    using CefSharp.WinForms;
    using System.Windows;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Interaction logic for CefSharpBrowser.xaml.
    /// </summary>
    public partial class CefSharpBrowser
    {
        #region Fields

        public static readonly DependencyProperty BackwardCommandProperty =
            DependencyProperty.Register(nameof(BackwardCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty ForwardCommandProperty =
            DependencyProperty.Register(nameof(ForwardCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty IsAirspaceVisibleProperty =
            DependencyProperty.Register(nameof(IsAirspaceVisible), typeof(bool), typeof(CefSharpBrowser), new PropertyMetadata(false));

        public static readonly DependencyProperty IsFullScreenCommandProperty =
            DependencyProperty.Register(nameof(IsFullScreenCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null, OnIsFullScreenCommandChanged));

        public static readonly DependencyProperty ReloadCommandProperty =
            DependencyProperty.Register(nameof(ReloadCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register(nameof(Url), typeof(string), typeof(CefSharpBrowser), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUrlChanged));

        public static readonly DependencyProperty WebBrowserProperty =
            DependencyProperty.Register(nameof(WebBrowser), typeof(IWebBrowser), typeof(CefSharpBrowser), new PropertyMetadata(null));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefSharpBrowser"/> class.
        /// </summary>
        public CefSharpBrowser()
        {
            Initialize();
            this.CefDisplayHandler = new CefDisplayHandler();
            InitializeComponent();
            this.WebBrowser = this.ChromiumWebBrowser;
            this.Loaded += this.OnLoaded;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackwardCommand.
        /// </summary>
        public ICommand BackwardCommand
        {
            get { return (ICommand)GetValue(BackwardCommandProperty); }
            set { SetValue(BackwardCommandProperty, value); }
        }

        /// <summary>
        /// Gets the CefSettings.
        /// </summary>
        public CefSettings CefSettings { get; }

        /// <summary>
        /// Gets or sets the ForwardCommand.
        /// </summary>
        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsAirspaceVisible.
        /// </summary>
        public bool IsAirspaceVisible
        {
            get { return (bool)GetValue(IsAirspaceVisibleProperty); }
            set { SetValue(IsAirspaceVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the IsFullScreenCommand.
        /// </summary>
        public ICommand IsFullScreenCommand
        {
            get { return (ICommand)GetValue(IsFullScreenCommandProperty); }
            set { SetValue(IsFullScreenCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ReloadCommand.
        /// </summary>
        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        /// <summary>
        /// Gets or sets the WebBrowser.
        /// </summary>
        public IWebBrowser WebBrowser
        {
            get { return (IWebBrowser)this.GetValue(WebBrowserProperty); }
            set { this.SetValue(WebBrowserProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanBackward.
        /// </summary>
        private bool CanBackward { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanForward.
        /// </summary>
        private bool CanForward { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanReload.
        /// </summary>
        private bool CanReload { get; set; }

        /// <summary>
        /// Gets the CefDisplayHandler.
        /// </summary>
        private CefDisplayHandler CefDisplayHandler { get; }

        /// <summary>
        /// Gets or sets the InternalUrl.
        /// </summary>
        private string InternalUrl { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Initialize.
        /// </summary>
        public static void Initialize()
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();
                Cef.Initialize(settings);
            }
        }

        /// <summary>
        /// The OnIsFullScreenCommandChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnIsFullScreenCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = (CefSharpBrowser)d;
            var isFullScreenCommand = (ICommand)e.NewValue;
            browser.CefDisplayHandler.IsFullScreenCommand = isFullScreenCommand;
        }

        /// <summary>
        /// The OnUrlChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = (CefSharpBrowser)d;
            if (!(e.NewValue is string url) || browser.InternalUrl == url)
            {
                return;
            }

            browser.WebBrowser.Load(url);
        }

        /// <summary>
        /// The OnBackward.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnBackward(object obj)
        {
            if (this.WebBrowser.CanGoBack)
            {
                this.WebBrowser.Back();
            }
        }

        /// <summary>
        /// The OnForward.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnForward(object obj)
        {
            if (this.WebBrowser.CanGoForward)
            {
                this.WebBrowser.Forward();
            }
        }

        /// <summary>
        /// The OnLoaded.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;
            this.WebBrowser.LoadingStateChanged += OnWebBrowser_LoadingStateChanged;
            this.WebBrowser.LoadError += OnWebBrowser_LoadError;
            this.WebBrowser.DisplayHandler = this.CefDisplayHandler;
            this.WebBrowser.KeyboardHandler = new CefKeyboardHandler(this.windowsFormsHost);
            this.ChromiumWebBrowser.AddressChanged += OnWebBrowser_AddressChanged;
            this.BackwardCommand = new RelayCommand(this.OnBackward, "Backward", (o) => this.CanBackward);
            this.ForwardCommand = new RelayCommand(this.OnForward, "Forward", (o) => this.CanForward);
            this.ReloadCommand = new RelayCommand(this.OnReload, "Reload", (o) => this.CanReload);
        }

        /// <summary>
        /// The OnReload.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnReload(object obj)
        {
            this.WebBrowser.Reload(true);
        }

        /// <summary>
        /// The OnWebBrowser_AddressChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="AddressChangedEventArgs"/>.</param>
        private void OnWebBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            UIThreadHelper.Invoke(() =>
            {
                this.InternalUrl = e.Address;
                this.Url = this.InternalUrl;
                CommandManager.InvalidateRequerySuggested();
            });
        }

        /// <summary>
        /// The OnWebBrowser_LoadError.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="LoadErrorEventArgs"/>.</param>
        private void OnWebBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
        }

        /// <summary>
        /// The OnWebBrowser_LoadingStateChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="LoadingStateChangedEventArgs"/>.</param>
        private void OnWebBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            UIThreadHelper.Invoke(() =>
            {
                this.CanBackward = e.CanGoBack;
                this.CanForward = e.CanGoForward;
                this.CanReload = e.CanReload;
            });
        }

        #endregion Methods
    }
}