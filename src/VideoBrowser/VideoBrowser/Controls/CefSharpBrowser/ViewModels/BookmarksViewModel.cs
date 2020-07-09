namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using System.Collections.ObjectModel;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Models;

    /// <summary>
    /// Defines the <see cref="BookmarksViewModel" />.
    /// </summary>
    public class BookmarksViewModel : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarksViewModel"/> class.
        /// </summary>
        /// <param name="bookmarkModels">The bookmarkModels<see cref="ObservableCollection{BookmarkModel}"/>.</param>
        internal BookmarksViewModel(ObservableCollection<BookmarkModel> bookmarkModels)
        {
            this.BookmarkModels = bookmarkModels;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BookmarkModels.
        /// </summary>
        public ObservableCollection<BookmarkModel> BookmarkModels { get; }

        #endregion Properties
    }
}