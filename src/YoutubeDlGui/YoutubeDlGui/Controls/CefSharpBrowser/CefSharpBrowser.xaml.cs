namespace YoutubeDlGui.Controls.CefSharpBrowser
{
    using CefSharp;
    using CefSharp.WinForms;
    using System.Windows;
    using System.Windows.Input;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Helpers;

    /// <summary>
    /// Interaction logic for CefSharpBrowser.xaml
    /// </summary>
    public partial class CefSharpBrowser
    {
        #region Fields

        public static readonly DependencyProperty BackwardCommandProperty =
            DependencyProperty.Register(nameof(BackwardCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty CanBackwardProperty =
            DependencyProperty.Register(nameof(CanBackward), typeof(bool), typeof(CefSharpBrowser), new PropertyMetadata(false));

        public static readonly DependencyProperty CanForwardProperty =
            DependencyProperty.Register(nameof(CanForward), typeof(bool), typeof(CefSharpBrowser), new PropertyMetadata(false));

        public static readonly DependencyProperty CanReloadProperty =
            DependencyProperty.Register(nameof(CanReload), typeof(bool), typeof(CefSharpBrowser), new PropertyMetadata(false));

        public static readonly DependencyProperty ForwardCommandProperty =
            DependencyProperty.Register(nameof(ForwardCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty ReloadCommandProperty =
            DependencyProperty.Register(nameof(ReloadCommand), typeof(ICommand), typeof(CefSharpBrowser), new PropertyMetadata(null));

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register(nameof(Url), typeof(string), typeof(CefSharpBrowser), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUrlChanged));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefSharpBrowser"/> class.
        /// </summary>
        public CefSharpBrowser()
        {
            this.CefSettings = new CefSettings();
            Cef.Initialize(this.CefSettings);
            InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackwardCommand
        /// </summary>
        public ICommand BackwardCommand
        {
            get { return (ICommand)GetValue(BackwardCommandProperty); }
            set { SetValue(BackwardCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanBackward
        /// </summary>
        public bool CanBackward
        {
            get { return (bool)GetValue(CanBackwardProperty); }
            set { SetValue(CanBackwardProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanForward
        /// </summary>
        public bool CanForward
        {
            get { return (bool)GetValue(CanForwardProperty); }
            set { SetValue(CanForwardProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanReload
        /// </summary>
        public bool CanReload
        {
            get { return (bool)GetValue(CanReloadProperty); }
            set { SetValue(CanReloadProperty, value); }
        }

        /// <summary>
        /// Gets the CefSettings
        /// </summary>
        public CefSettings CefSettings { get; }

        /// <summary>
        /// Gets or sets the ForwardCommand
        /// </summary>
        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ReloadCommand
        /// </summary>
        public ICommand ReloadCommand
        {
            get { return (ICommand)GetValue(ReloadCommandProperty); }
            set { SetValue(ReloadCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        /// <summary>
        /// Gets or sets the InternalUrl
        /// </summary>
        private string InternalUrl { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnUrlChanged
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/></param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/></param>
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
        /// The OnBackward
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnBackward(object obj)
        {
            if (this.WebBrowser.CanGoBack)
            {
                this.WebBrowser.Back();
            }
        }

        /// <summary>
        /// The OnForward
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnForward(object obj)
        {
            if (this.WebBrowser.CanGoForward)
            {
                this.WebBrowser.Forward();
            }
        }

        /// <summary>
        /// The OnLoaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;
            this.WebBrowser.LoadingStateChanged += OnWebBrowser_LoadingStateChanged;
            this.WebBrowser.AddressChanged += OnWebBrowser_AddressChanged;
            this.WebBrowser.LoadError += OnWebBrowser_LoadError;
            this.BackwardCommand = new RelayCommand(this.OnBackward);
            this.ForwardCommand = new RelayCommand(this.OnForward);
            this.ReloadCommand = new RelayCommand(this.OnReload);
        }

        /// <summary>
        /// The OnReload
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnReload(object obj)
        {
            this.WebBrowser.Reload(true);
        }

        /// <summary>
        /// The OnWebBrowser_AddressChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="AddressChangedEventArgs"/></param>
        private void OnWebBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            UIThreadHelper.Invoke(() =>
            {
                this.InternalUrl = e.Address;
                this.Url = this.InternalUrl;
            });
        }

        /// <summary>
        /// The OnWebBrowser_LoadError
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="LoadErrorEventArgs"/></param>
        private void OnWebBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
        }

        /// <summary>
        /// The OnWebBrowser_LoadingStateChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="LoadingStateChangedEventArgs"/></param>
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