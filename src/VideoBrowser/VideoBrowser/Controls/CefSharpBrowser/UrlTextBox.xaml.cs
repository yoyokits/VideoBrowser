namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.AddIns;
    using VideoBrowser.Controls.CefSharpBrowser.Models;

    /// <summary>
    /// Interaction logic for UrlTextBox.xaml.
    /// </summary>
    public partial class UrlTextBox
    {
        #region Fields

        public static readonly DependencyProperty AddInButtonClickedProperty =
            DependencyProperty.Register(nameof(AddInButtonClicked), typeof(ICommand), typeof(UrlTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty BookmarkModelsProperty =
            DependencyProperty.Register(nameof(BookmarkModels), typeof(IList<BookmarkModel>), typeof(UrlTextBox), new PropertyMetadata(null, OnBookmarkModelsChanged));

        public static readonly DependencyProperty LeftAddInButtonsProperty =
            DependencyProperty.Register(nameof(LeftAddInButtons), typeof(ObservableCollection<AddInButton>), typeof(UrlTextBox), new PropertyMetadata());

        public static readonly DependencyProperty NavigateUrlCommandProperty =
            DependencyProperty.Register(nameof(NavigateUrlCommand), typeof(ICommand), typeof(UrlTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty NavigateUrlProperty =
            DependencyProperty.Register(nameof(NavigateUrl), typeof(string), typeof(UrlTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNavigateUrlChanged));

        public static readonly DependencyProperty RightAddInButtonsProperty =
            DependencyProperty.Register(nameof(RightAddInButtons), typeof(ObservableCollection<AddInButton>), typeof(UrlTextBox), new PropertyMetadata());

        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register(nameof(Url), typeof(string), typeof(UrlTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUrlChanged));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlTextBox"/> class.
        /// </summary>
        public UrlTextBox()
        {
            this.BookmarkAddIn = new BookmarkAddIn();
            this.IsSecureAddIn = new IsSecureAddIn();
            this.InternalNavigateUrlCommand = new RelayCommand(this.OnNavigateUrl, nameof(this.InternalNavigateUrlCommand));
            this.InitializeComponent();
            this.LeftAddInButtons = new ObservableCollection<AddInButton> { this.IsSecureAddIn };
            this.RightAddInButtons = new ObservableCollection<AddInButton> { this.BookmarkAddIn };
            this.TextBox.InputBindings.Add(new KeyBinding(this.InternalNavigateUrlCommand, Key.Enter, ModifierKeys.None));
            this.GotFocus += this.OnUrlTextBox_GotFocus;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the AddInButtonClicked.
        /// </summary>
        public ICommand AddInButtonClicked
        {
            get { return (ICommand)GetValue(AddInButtonClickedProperty); }
            set { SetValue(AddInButtonClickedProperty, value); }
        }

        /// <summary>
        /// Gets the BookmarkAddIn.
        /// </summary>
        public BookmarkAddIn BookmarkAddIn { get; }

        /// <summary>
        /// Gets or sets the BookmarkModels.
        /// </summary>
        public IList<BookmarkModel> BookmarkModels { get => (IList<BookmarkModel>)GetValue(BookmarkModelsProperty); set => SetValue(BookmarkModelsProperty, value); }

        /// <summary>
        /// Gets the IsSecureAddIn.
        /// </summary>
        public IsSecureAddIn IsSecureAddIn { get; }

        /// <summary>
        /// Gets or sets the LeftAddInButtons.
        /// </summary>
        public ObservableCollection<AddInButton> LeftAddInButtons { get => (ObservableCollection<AddInButton>)GetValue(LeftAddInButtonsProperty); set => SetValue(LeftAddInButtonsProperty, value); }

        /// <summary>
        /// Gets or sets the NavigateUrl.
        /// </summary>
        public string NavigateUrl { get => (string)GetValue(NavigateUrlProperty); set => SetValue(NavigateUrlProperty, value); }

        /// <summary>
        /// Gets or sets the NavigateUrlCommand.
        /// </summary>
        public ICommand NavigateUrlCommand { get => (ICommand)GetValue(NavigateUrlCommandProperty); set => SetValue(NavigateUrlCommandProperty, value); }

        /// <summary>
        /// Gets or sets the RightAddInButtons.
        /// </summary>
        public ObservableCollection<AddInButton> RightAddInButtons { get => (ObservableCollection<AddInButton>)GetValue(RightAddInButtonsProperty); set => SetValue(RightAddInButtonsProperty, value); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => (string)GetValue(UrlProperty); set => SetValue(UrlProperty, value); }

        /// <summary>
        /// Gets the InternalNavigateUrlCommand.
        /// </summary>
        private ICommand InternalNavigateUrlCommand { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnBookmarkModelsChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnBookmarkModelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (UrlTextBox)d;
            textBox.BookmarkAddIn.BookmarkModels = e.NewValue as IList<BookmarkModel>;
        }

        /// <summary>
        /// The OnNavigateUrlChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnNavigateUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (UrlTextBox)d;
            var navigateUrl = e.NewValue.ToString();
            textBox.BookmarkAddIn.Url = navigateUrl;
            textBox.IsSecureAddIn.Url = navigateUrl;
            textBox.Url = navigateUrl;
        }

        /// <summary>
        /// The OnUrlChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (UrlTextBox)d;
            textBox.BookmarkAddIn.Url = string.Empty;
            textBox.IsSecureAddIn.Url = string.Empty;
        }

        /// <summary>
        /// The OnNavigateUrl.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnNavigateUrl(object obj)
        {
            this.BookmarkAddIn.Url = this.Url;
            this.IsSecureAddIn.Url = this.Url;
            this.NavigateUrlCommand?.Execute(obj);
        }

        /// <summary>
        /// The OnUrlTextBox_GotFocus.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void OnUrlTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TextBox.Focus();
        }

        #endregion Methods
    }
}