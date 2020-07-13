namespace VideoBrowser.Controls.CefSharpBrowser.AddIns
{
    using System;
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

        private string _url;

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
        public IList<BookmarkModel> BookmarkModels { get; internal set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url
        {
            get => _url;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _url, value))
                {
                    return;
                }

                this.IsVisible = !string.IsNullOrEmpty(this.Url);
                this.UpdateIcon();
            }
        }

        /// <summary>
        /// Gets or sets the ClickAction.
        /// </summary>
        internal Action<BookmarkModel> ClickAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected override void Execute(WebBrowserTabControlViewModel viewModel)
        {
            if (this.BookmarkModels == null)
            {
                return;
            }

            lock (this.BookmarkModels)
            {
                foreach (var item in this.BookmarkModels)
                {
                    if (item.Url == this.Url)
                    {
                        return;
                    }
                }

                var bookmark = new BookmarkModel
                {
                    Url = this.Url
                };

                this.BookmarkModels.Insert(0, bookmark);
            }

            this.Icon = BrowserIcons.Star;
        }

        /// <summary>
        /// The UpdateIcon.
        /// </summary>
        private void UpdateIcon()
        {
            if (this.BookmarkModels != null)
            {
                lock (this.BookmarkModels)
                {
                    foreach (var bookmark in this.BookmarkModels)
                    {
                        if (bookmark.Url == this.Url)
                        {
                            this.Icon = BrowserIcons.Star;
                            return;
                        }
                    }
                }
            }

            this.Icon = BrowserIcons.StarWF;
        }

        #endregion Methods
    }
}