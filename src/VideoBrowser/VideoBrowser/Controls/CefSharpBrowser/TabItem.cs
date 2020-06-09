namespace VideoBrowser.Controls.CefSharpBrowser
{
    using Dragablz;
    using System;
    using System.Windows.Media;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="TabItem" />.
    /// </summary>
    public class TabItem : HeaderedItemViewModel, IDisposable
    {
        #region Fields

        private Geometry _icon;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItem"/> class.
        /// </summary>
        public TabItem() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItem"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public TabItem(Guid guid)
        {
            this.Guid = guid;
            this.HeaderViewModel = new WebBrowserTabHeaderViewModel { Header = "No Header" };
            UIThreadHelper.Invoke(() =>
            {
                this.Header = new WebBrowserTabHeaderView { DataContext = this.HeaderViewModel };
            });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether Disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Gets the HeaderViewModel.
        /// </summary>
        public WebBrowserTabHeaderViewModel HeaderViewModel { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon
        {
            get => _icon;
            set
            {
                if (_icon == value)
                {
                    return;
                }

                _icon = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title
        {
            get => this.HeaderViewModel.Header;
            set
            {
                if (this.Title == value)
                {
                    return;
                }

                this.HeaderViewModel.Header = value;
                this.OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.Disposed)
            {
                return;
            }

            this.Disposed = true;
            this.Dispose(true);
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode() => this.Guid.GetHashCode();

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.Content = null;
            this.Header = null;
        }

        #endregion Methods
    }
}