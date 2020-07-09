namespace VideoBrowser.Controls.CefSharpBrowser.AddIns
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using VideoBrowser.Common;
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

        private IList<BookmarkModel> _bookmarkModels;

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
            this.ClickCommand = new RelayCommand(this.OnClick, $"{this.Name}.{nameof(this.ClickCommand)}");
            this.RemoveCommand = new RelayCommand(this.OnRemove, $"{this.Name}.{nameof(this.RemoveCommand)}");
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BookmarkModels.
        /// </summary>
        public IList<BookmarkModel> BookmarkModels
        {
            get => _bookmarkModels;
            internal set
            {
                if (_bookmarkModels == value)
                {
                    return;
                }

                _bookmarkModels = value;
                if (this.BookmarkModels != null)
                {
                    foreach (var bookmark in this.BookmarkModels)
                    {
                        bookmark.ClickCommand = this.ClickCommand;
                        bookmark.RemoveCommand = this.RemoveCommand;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the ClickCommand.
        /// </summary>
        public ICommand ClickCommand { get; }

        /// <summary>
        /// Gets the RemoveCommand.
        /// </summary>
        public ICommand RemoveCommand { get; }

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
                var bookmarkExist = false;
                if (this.BookmarkModels != null)
                {
                    lock (this.BookmarkModels)
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
                }

                this.Icon = bookmarkExist ? BrowserIcons.Star : BrowserIcons.StarWF;
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
                    ClickCommand = this.ClickCommand,
                    RemoveCommand = this.RemoveCommand,
                    Url = this.Url
                };

                this.BookmarkModels.Insert(0, bookmark);
            }
        }

        /// <summary>
        /// The OnClick.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClick(object obj)
        {
        }

        /// <summary>
        /// The OnRemove.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnRemove(object obj)
        {
        }

        #endregion Methods
    }
}