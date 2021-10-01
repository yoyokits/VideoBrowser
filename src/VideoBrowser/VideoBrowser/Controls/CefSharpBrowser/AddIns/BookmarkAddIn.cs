namespace VideoBrowser.Controls.CefSharpBrowser.AddIns
{
    using System.Collections.Generic;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Controls.CefSharpBrowser.Resources;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="BookmarkAddIn" />.
    /// </summary>
    public class BookmarkAddIn : AddInButton
    {
        #region Fields

        private string url;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarkAddIn"/> class.
        /// </summary>
        internal BookmarkAddIn()
        {
            this.Icon = BrowserIcons.StarWF;
            this.ToolTip = "Bookmark this page";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BookmarkModels.
        /// </summary>
        public IList<BookmarkModel> BookmarkModels { get; set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url
        {
            get => url;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref url, value))
                {
                    return;
                }

                this.IsVisible = !string.IsNullOrEmpty(this.Url);
                var bookmarkExist = false;
                if (this.BookmarkModels != null)
                {
                    foreach (var bookmark in this.BookmarkModels)
                    {
                        if (bookmark.Url == this.Url)
                        {
                            bookmarkExist = true;
                            break;
                        }
                    }
                }

                this.Icon = bookmarkExist ? BrowserIcons.Star : BrowserIcons.StarWF;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected override void Execute(WebBrowserTabControlViewModel viewModel)
        {
        }

        #endregion Methods
    }
}