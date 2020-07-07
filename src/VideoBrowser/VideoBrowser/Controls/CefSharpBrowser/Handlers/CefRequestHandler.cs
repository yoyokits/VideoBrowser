namespace VideoBrowser.Controls.CefSharpBrowser.Handlers
{
    using CefSharp;
    using CefSharp.Handler;
    using System;

    /// <summary>
    /// Defines the <see cref="CefRequestHandler" />.
    /// </summary>
    public class CefRequestHandler : RequestHandler
    {
        #region Properties

        /// <summary>
        /// Gets or sets the OpenUrlFromTabAction.
        /// </summary>
        internal Action<string> OpenUrlFromTabAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnOpenUrlFromTab.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="frame">The frame<see cref="IFrame"/>.</param>
        /// <param name="targetUrl">The targetUrl<see cref="string"/>.</param>
        /// <param name="targetDisposition">The targetDisposition<see cref="WindowOpenDisposition"/>.</param>
        /// <param name="userGesture">The userGesture<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected override bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            this.OpenUrlFromTabAction?.Invoke(targetUrl);
            return true;
        }

        #endregion Methods
    }
}