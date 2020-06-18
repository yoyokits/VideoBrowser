namespace VideoBrowser.Controls.CefSharpBrowser
{
    using CefSharp;
    using CefSharp.Wpf;
    using System;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="CefContextMenuHandler" />.
    /// </summary>
    public class CefContextMenuHandler : IContextMenuHandler
    {
        #region Constants

        private const CefMenuCommand CopyLinkAdressId = (CefMenuCommand)10000;

        private const CefMenuCommand OpenImageInNewTabId = (CefMenuCommand)10001;

        private const CefMenuCommand OpenInNewTabId = (CefMenuCommand)10002;

        private const CefMenuCommand OpenInNewWindowId = (CefMenuCommand)10003;

        private const CefMenuCommand SearchInWebsiteId = (CefMenuCommand)10004;

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
        /// Gets or sets the SearchEngineQuery.
        /// </summary>
        public string SearchEngineQuery { get; set; } = "https://www.youtube.com/results?search_query=";

        /// <summary>
        /// Gets the Copy.
        /// </summary>
        private static (CefMenuCommand Id, string Text) Copy { get; } = (CefMenuCommand.Copy, "Copy");

        /// <summary>
        /// Gets the CopyLinkAddress.
        /// </summary>
        private static (CefMenuCommand Id, string Text) CopyLinkAddress { get; } = (CopyLinkAdressId, "Copy link address");

        /// <summary>
        /// Gets the OpenImageInNewTab.
        /// </summary>
        private static (CefMenuCommand Id, string Text) OpenImageInNewTab { get; } = (OpenImageInNewTabId, "Open image in new tab");

        /// <summary>
        /// Gets the OpenInNewTab.
        /// </summary>
        private static (CefMenuCommand Id, string Text) OpenInNewTab { get; } = (OpenInNewTabId, "Open in new tab");

        /// <summary>
        /// Gets the OpenInNewWindow.
        /// </summary>
        private static (CefMenuCommand Id, string Text) OpenInNewWindow { get; } = (OpenInNewWindowId, "Open in new window");

        /// <summary>
        /// Gets the Paste.
        /// </summary>
        private static (CefMenuCommand Id, string Text) Paste { get; } = (CefMenuCommand.Paste, "Paste");

        /// <summary>
        /// Gets the SearchInWebsite.
        /// </summary>
        private static (CefMenuCommand Id, string Text) SearchInWebsite { get; } = (SearchInWebsiteId, "Search in Youtube");

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
            var sourceUrlExist = !string.IsNullOrEmpty(parameters.SourceUrl);

            if (linkExist || selectionTextExist)
            {
                model.AddSeparator();
            }

            model.Clear();
            if (linkExist)
            {
                this.AddItem(model, CopyLinkAddress.Id, CopyLinkAddress.Text);
                this.AddItem(model, OpenInNewTab.Id, OpenInNewTab.Text);
                this.AddItem(model, OpenInNewWindow.Id, OpenInNewWindow.Text);
            }

            if (sourceUrlExist)
            {
                if (UrlHelper.IsImageUrl(parameters.SourceUrl))
                {
                    this.AddItem(model, OpenImageInNewTab.Id, OpenImageInNewTab.Text);
                }
            }

            if (selectionTextExist)
            {
                this.AddItem(model, Copy.Id, Copy.Text);
                this.AddItem(model, SearchInWebsite.Id, SearchInWebsite.Text);
            }

            if ((parameters.EditStateFlags & ContextMenuEditState.CanPaste) != ContextMenuEditState.None)
            {
                this.AddItem(model, Paste.Id, Paste.Text);
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
                case CefMenuCommand.Copy:
                    Clipboard.SetText(parameters.SelectionText);
                    break;

                case CefMenuCommand.Paste:
                    chromiumWebBrowser.Paste();
                    break;

                case CopyLinkAdressId:
                    Clipboard.SetText(parameters.LinkUrl);
                    break;

                case OpenImageInNewTabId:
                    this.OpenInNewTabAction?.Invoke(parameters.SourceUrl);
                    break;

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
            var webBrowser = (ChromiumWebBrowser)chromiumWebBrowser;
            webBrowser.Dispatcher.Invoke(() =>
            {
                webBrowser.ContextMenu = null;
            });
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

        /// <summary>
        /// The AddItem.
        /// </summary>
        /// <param name="model">The model<see cref="IMenuModel"/>.</param>
        /// <param name="id">The id<see cref="CefMenuCommand"/>.</param>
        /// <param name="label">The label<see cref="string"/>.</param>
        private void AddItem(IMenuModel model, CefMenuCommand id, string label)
        {
            model.AddItem(id, label);
            model.SetEnabled(id, true);
        }

        #endregion Methods
    }
}