namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using CefSharp.Structs;
    using CefSharp.Wpf;
    using System.Collections.Generic;
    using System.Windows.Input;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="CefDisplayHandler" />.
    /// </summary>
    public class CefDisplayHandler : IDisplayHandler
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefDisplayHandler"/> class.
        /// </summary>
        internal CefDisplayHandler()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the IsFullScreenCommand.
        /// </summary>
        internal ICommand IsFullScreenCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnAutoResize.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="newSize">The newSize<see cref="Size"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, Size newSize)
        {
            return true;
        }

        /// <summary>
        /// The OnLoadingProgressChange.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="progress">The progress<see cref="double"/>.</param>
        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
        }

        /// <summary>
        /// The OnTooltipChanged.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="text">The text<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }

        /// <summary>
        /// The OnAddressChanged.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="addressChangedArgs">The addressChangedArgs<see cref="AddressChangedEventArgs"/>.</param>
        void IDisplayHandler.OnAddressChanged(IWebBrowser browserControl, AddressChangedEventArgs addressChangedArgs)
        {
        }

        /// <summary>
        /// The OnConsoleMessage.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="consoleMessageArgs">The consoleMessageArgs<see cref="ConsoleMessageEventArgs"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IDisplayHandler.OnConsoleMessage(IWebBrowser browserControl, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }

        /// <summary>
        /// The OnFaviconUrlChange.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="urls">The urls<see cref="IList{string}"/>.</param>
        void IDisplayHandler.OnFaviconUrlChange(IWebBrowser browserControl, IBrowser browser, IList<string> urls)
        {
        }

        /// <summary>
        /// The OnFullscreenModeChange.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="fullscreen">The fullscreen<see cref="bool"/>.</param>
        void IDisplayHandler.OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            chromiumWebBrowser.Invoke(() =>
            {
                this.IsFullScreenCommand.Execute(fullscreen);
                ////if (fullscreen)
                ////{
                ////    parent = chromiumWebBrowser.Parent;
                ////    parent.Controls.Remove(chromiumWebBrowser);

                ////    fullScreenForm = new Form
                ////    {
                ////        FormBorderStyle = FormBorderStyle.None,
                ////        WindowState = FormWindowState.Maximized
                ////    };
                ////    fullScreenForm.Controls.Add(chromiumWebBrowser);
                ////    fullScreenForm.ShowDialog(parent.FindForm());
                ////}
                ////else
                ////{
                ////    fullScreenForm.Controls.Remove(chromiumWebBrowser);
                ////    parent.Controls.Add(chromiumWebBrowser);
                ////    fullScreenForm.Close();
                ////    fullScreenForm.Dispose();
                ////    fullScreenForm = null;
                ////}
            });
        }

        /// <summary>
        /// The OnStatusMessage.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="statusMessageArgs">The statusMessageArgs<see cref="StatusMessageEventArgs"/>.</param>
        void IDisplayHandler.OnStatusMessage(IWebBrowser browserControl, StatusMessageEventArgs statusMessageArgs)
        {
        }

        /// <summary>
        /// The OnTitleChanged.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="titleChangedArgs">The titleChangedArgs<see cref="TitleChangedEventArgs"/>.</param>
        void IDisplayHandler.OnTitleChanged(IWebBrowser browserControl, TitleChangedEventArgs titleChangedArgs)
        {
        }

        #endregion Methods
    }
}