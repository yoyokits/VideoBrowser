namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using CefSharp.Wpf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using VideoBrowser.Common;
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

            ////model.Clear();
            if (linkExist)
            {
                this.AddItem(model, CopyLinkAddress.Id, CopyLinkAddress.Text);
                this.AddItem(model, OpenInNewTab.Id, OpenInNewTab.Text);
                this.AddItem(model, OpenInNewWindow.Id, OpenInNewWindow.Text);
            }

            if (sourceUrlExist && (parameters.SourceUrl != parameters.PageUrl))
            {
                if (parameters.SourceUrl.IsImageUrl())
                {
                    this.AddItem(model, OpenImageInNewTab.Id, OpenImageInNewTab.Text);
                }
            }

            if (selectionTextExist)
            {
                this.AddItem(model, SearchInWebsite.Id, SearchInWebsite.Text);
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
        /// The GetMenuItems.
        /// </summary>
        /// <param name="model">The model<see cref="IMenuModel"/>.</param>
        /// <returns>The <see cref="IEnumerable{Tuple{string, CefMenuCommand, bool}}"/>.</returns>
        private static IEnumerable<Tuple<string, CefMenuCommand, bool>> GetMenuItems(IMenuModel model)
        {
            for (var i = 0; i < model.Count; i++)
            {
                var header = model.GetLabelAt(i);
                var commandId = model.GetCommandIdAt(i);
                var isEnabled = model.IsEnabledAt(i);
                yield return new Tuple<string, CefMenuCommand, bool>(header, commandId, isEnabled);
            }
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
        bool IContextMenuHandler.RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            //NOTE: Return false to use the built in Context menu - in WPF this requires you integrate into your existing message loop, read the General Usage Guide for more details
            //https://github.com/cefsharp/CefSharp/wiki/General-Usage#multithreadedmessageloop
            //return false;

            var webBrowser = (ChromiumWebBrowser)chromiumWebBrowser;

            //IMenuModel is only valid in the context of this method, so need to read the values before invoking on the UI thread
            var menuItems = GetMenuItems(model).ToList();
            var linkUrl = parameters.LinkUrl;
            var sourceUrl = parameters.SourceUrl;
            var selectionText = parameters.SelectionText;

            webBrowser.Dispatcher.Invoke(() =>
            {
                var menu = new ContextMenu
                {
                    IsOpen = true
                };

                RoutedEventHandler handler = null;

                handler = (s, e) =>
                {
                    menu.Closed -= handler;

                    //If the callback has been disposed then it's already been executed
                    //so don't call Cancel
                    if (!callback.IsDisposed)
                    {
                        callback.Cancel();
                    }
                };

                menu.Closed += handler;

                foreach (var item in menuItems)
                {
                    if (item.Item2 == CefMenuCommand.NotFound && string.IsNullOrWhiteSpace(item.Item1))
                    {
                        menu.Items.Add(new Separator());
                        continue;
                    }

                    menu.Items.Add(new MenuItem
                    {
                        Header = item.Item1.Replace("&", "_"),
                        IsEnabled = item.Item3,
                        Command = new RelayCommand((o) =>
                        {
                            //BUG: CEF currently not executing callbacks correctly so we manually map the commands below
                            //see https://github.com/cefsharp/CefSharp/issues/1767
                            //The following line worked in previous versions, it doesn't now, so custom EXAMPLE below
                            //callback.Continue(item.Item2, CefEventFlags.None);

                            //NOTE: Note all menu item options below have been tested, you can work out the rest
                            switch (item.Item2)
                            {
                                ////case CefMenuCommand.Copy:
                                ////    Clipboard.SetText(parameters.SelectionText);
                                ////    break;

                                ////case CefMenuCommand.Paste:
                                ////    chromiumWebBrowser.Paste();
                                ////    break;

                                case CopyLinkAdressId:
                                    Clipboard.SetText(linkUrl);
                                    break;

                                case OpenImageInNewTabId:
                                    this.OpenInNewTabAction?.Invoke(sourceUrl);
                                    break;

                                case OpenInNewWindowId:
                                    this.OpenInNewWindowAction?.Invoke(linkUrl);
                                    break;

                                case OpenInNewTabId:
                                    this.OpenInNewTabAction?.Invoke(linkUrl);
                                    break;

                                case SearchInWebsiteId:
                                    var searchUrl = $"{this.SearchEngineQuery}{selectionText}";
                                    this.OpenInNewTabAction?.Invoke(searchUrl);
                                    break;

                                case CefMenuCommand.Back:
                                    {
                                        browser.GoBack();
                                        break;
                                    }
                                case CefMenuCommand.Forward:
                                    {
                                        browser.GoForward();
                                        break;
                                    }
                                case CefMenuCommand.Cut:
                                    {
                                        browser.FocusedFrame.Cut();
                                        break;
                                    }

                                case CefMenuCommand.Copy:
                                    {
                                        browser.FocusedFrame.Copy();
                                        break;
                                    }
                                case CefMenuCommand.Paste:
                                    {
                                        browser.FocusedFrame.Paste();
                                        break;
                                    }
                                case CefMenuCommand.Print:
                                    {
                                        browser.GetHost().Print();
                                        break;
                                    }
                                case CefMenuCommand.ViewSource:
                                    {
                                        browser.FocusedFrame.ViewSource();
                                        break;
                                    }
                                case CefMenuCommand.Undo:
                                    {
                                        browser.FocusedFrame.Undo();
                                        break;
                                    }
                                case CefMenuCommand.StopLoad:
                                    {
                                        browser.StopLoad();
                                        break;
                                    }
                                case CefMenuCommand.SelectAll:
                                    {
                                        browser.FocusedFrame.SelectAll();
                                        break;
                                    }
                                case CefMenuCommand.Redo:
                                    {
                                        browser.FocusedFrame.Redo();
                                        break;
                                    }
                                case CefMenuCommand.Find:
                                    {
                                        browser.GetHost().Find(parameters.SelectionText, true, false, false);
                                        break;
                                    }
                                case CefMenuCommand.AddToDictionary:
                                    {
                                        browser.GetHost().AddWordToDictionary(parameters.MisspelledWord);
                                        break;
                                    }
                                case CefMenuCommand.Reload:
                                    {
                                        browser.Reload();
                                        break;
                                    }
                                case CefMenuCommand.ReloadNoCache:
                                    {
                                        browser.Reload(ignoreCache: true);
                                        break;
                                    }
                                case (CefMenuCommand)26501:
                                    {
                                        browser.GetHost().ShowDevTools();
                                        break;
                                    }
                                case (CefMenuCommand)26502:
                                    {
                                        browser.GetHost().CloseDevTools();
                                        break;
                                    }
                            }
                        }, "")
                    });
                }
                webBrowser.ContextMenu = menu;
            });

            return true;
        }

        #endregion Methods
    }
}