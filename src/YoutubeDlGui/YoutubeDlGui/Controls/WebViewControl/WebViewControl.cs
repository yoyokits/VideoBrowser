namespace YoutubeDlGui.Controls.WebViewControl
{
    using Microsoft.Toolkit.Wpf.UI.Controls;
    using System;
    using System.Windows;
    using System.Windows.Input;
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="WebViewControl" />
    /// </summary>
    public class WebViewControl : WebViewCompatible
    {
        #region Fields

        public static readonly DependencyProperty BackwardCommandProperty =
            DependencyProperty.Register(nameof(BackwardCommand), typeof(ICommand), typeof(WebViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty CanBackwardProperty =
            DependencyProperty.Register(nameof(CanBackward), typeof(bool), typeof(WebViewControl), new PropertyMetadata(false));

        public static readonly DependencyProperty CanForwardProperty =
            DependencyProperty.Register(nameof(CanForward), typeof(bool), typeof(WebViewControl), new PropertyMetadata(false));

        public static readonly DependencyProperty ForwardCommandProperty =
            DependencyProperty.Register(nameof(ForwardCommand), typeof(ICommand), typeof(WebViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register(nameof(Url), typeof(string), typeof(WebViewControl), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUrlChanged));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebViewControl"/> class.
        /// </summary>
        public WebViewControl()
        {
            this.ContentLoading += OnWebViewControl_ContentLoading;
            this.Loaded += this.OnLoaded;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackwardCommand
        /// Gets the BackwardCommand
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
        /// Gets or sets the ForwardCommand
        /// </summary>
        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnUrlChanged
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/></param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/></param>
        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var url = e.NewValue as string;
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var control = (WebViewControl)d;
            if (!url.Contains("http"))
            {
                url = $@"http:\\{url}";
            }
            var isCorrect = Uri.TryCreate(url, UriKind.Absolute, out Uri navigateUri)
                && (navigateUri.Scheme == Uri.UriSchemeHttp || navigateUri.Scheme == Uri.UriSchemeHttps);

            if (isCorrect)
            {
                control.Navigate(navigateUri);
            }
        }

        /// <summary>
        /// The OnBackward
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnBackward(object obj)
        {
            this.GoBack();
        }

        /// <summary>
        /// The OnForward
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnForward(object obj)
        {
            this.GoForward();
        }

        /// <summary>
        /// The OnLoaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;
            this.BackwardCommand = new RelayCommand(this.OnBackward, "Backward", (o) => this.CanBackward);
            this.ForwardCommand = new RelayCommand(this.OnForward, "Forward", (o) => this.CanForward);
        }

        /// <summary>
        /// The WebViewControl_ContentLoading
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlContentLoadingEventArgs"/></param>
        private void OnWebViewControl_ContentLoading(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlContentLoadingEventArgs e)
        {
            this.Url = e.Uri.AbsoluteUri;
            this.CanBackward = this.CanGoBack;
            this.CanForward = this.CanGoForward;
        }

        #endregion Methods
    }
}