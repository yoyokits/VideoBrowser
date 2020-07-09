namespace VideoBrowser.Controls.CefSharpBrowser.AddIns
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Controls.CefSharpBrowser.Resources;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;

    /// <summary>
    /// Defines the <see cref="ShowBookmarksButton" />.
    /// </summary>
    internal class ShowBookmarksButton : CreateTabAddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowBookmarksButton"/> class.
        /// </summary>
        /// <param name="bookmarkModels">The bookmarkModels<see cref="ObservableCollection{BookmarkModel}"/>.</param>
        internal ShowBookmarksButton(ObservableCollection<BookmarkModel> bookmarkModels) : base("Bookmarks", BrowserIcons.Star)
        {
            this.BookmarksViewModel = new BookmarksViewModel(bookmarkModels);
            this.ToolTip = "Show Bookmarks";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BookmarksViewModel.
        /// </summary>
        public BookmarksViewModel BookmarksViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateView.
        /// </summary>
        /// <returns>The <see cref="UIElement"/>.</returns>
        protected override UIElement CreateView()
        {
            var bookmarksView = new BookmarksView { DataContext = this.BookmarksViewModel };
            return bookmarksView;
        }

        #endregion Methods
    }
}