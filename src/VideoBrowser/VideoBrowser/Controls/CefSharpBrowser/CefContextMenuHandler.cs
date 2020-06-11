namespace VideoBrowser.Controls.CefSharpBrowser
{
    using CefSharp;
    using System;

    /// <summary>
    /// Defines the <see cref="CefContextMenuHandler" />.
    /// </summary>
    public class CefContextMenuHandler : IContextMenuHandler
    {
        #region Constants

        private const CefMenuCommand OpenInNewTabId = (CefMenuCommand)10000;

        private const CefMenuCommand OpenInNewWindowId = (CefMenuCommand)10001;

        private const CefMenuCommand SearchInWebsiteId = (CefMenuCommand)10002;

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets or sets the OpenInNewTabAction.
        /// </summary>
        public Action<string> OpenInNewTabAction { get; set; }

        /// <summary>
        /// Gets or sets the OpenInNewWindowAction.
        /// </summary>
        public Action<string> OpenInNewWindowAction { get; set; }

        /// <summary>
        /// Gets the OpenInNewTab.
        /// </summary>
        private static (CefMenuCommand Id, string Text) OpenInNewTab { get; } = (OpenInNewTabId, "Open in new tab");

        /// <summary>
        /// Gets the OpenInNewWindow.
        /// </summary>
        private static (CefMenuCommand Id, string Text) OpenInNewWindow { get; } = (OpenInNewWindowId, "Open in new window");

        /// <summary>
        /// Gets the SearchInWebsite.
        /// </summary>
        private static (CefMenuCommand Id, string Text) SearchInWebsite { get; } = (SearchInWebsiteId, "Search in Youtube");

        public string SearchEngineQuery { get; set; } = "https://www.youtube.com/results?search_query=";

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnBeforeContextMenu.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="IFrame"/>.</param>
        /// <param name="parameters">The parameters<see cref="IContextMenuParams"/>.</param>
        /// <param name="model">The model<see cref="IMenuModel"/>.</param>
        public virtual void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            var linkExist = !string.IsNullOrEmpty(parameters.LinkUrl);
            var selectionTextExist = !string.IsNullOrEmpty(parameters.SelectionText);

            if (linkExist || selectionTextExist)
            {
                model.AddSeparator();
            }

            if (linkExist)
            {
                model.AddItem(OpenInNewTab.Id, OpenInNewTab.Text);
                model.AddItem(OpenInNewWindow.Id, OpenInNewWindow.Text);
                model.SetEnabled(OpenInNewTab.Id, true);
                model.SetEnabled(OpenInNewWindow.Id, true);
            }

            if (selectionTextExist)
            {
                model.AddItem(SearchInWebsite.Id, SearchInWebsite.Text);
                model.SetEnabled(SearchInWebsite.Id, true);
            }
        }

        /// <summary>
        /// The OnContextMenuCommand.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="IFrame"/>.</param>
        /// <param name="parameters">The parameters<see cref="IContextMenuParams"/>.</param>
        /// <param name="commandId">The commandId<see cref="CefMenuCommand"/>.</param>
        /// <param name="eventFlags">The eventFlags<see cref="CefEventFlags"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            switch (commandId)
            {
                case OpenInNewWindowId:
                    this.OpenInNewWindowAction?.Invoke(parameters.LinkUrl);
                    break;

                case OpenInNewTabId:
                    this.OpenInNewTabAction?.Invoke(parameters.LinkUrl);
                    break;

                case SearchInWebsiteId:
                    var searchUrl = $"{this.SearchEngineQuery}{parameters.SelectionText}";
                    this.OpenInNewTabAction?.Invoke(searchUrl);
                    break;

                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// The OnContextMenuDismissed.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="IFrame"/>.</param>
        public virtual void OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {
        }

        /// <summary>
        /// The RunContextMenu.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="IFrame"/>.</param>
        /// <param name="parameters">The parameters<see cref="IContextMenuParams"/>.</param>
        /// <param name="model">The model<see cref="IMenuModel"/>.</param>
        /// <param name="callback">The callback<see cref="IRunContextMenuCallback"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }

        #endregion Methods
    }
}