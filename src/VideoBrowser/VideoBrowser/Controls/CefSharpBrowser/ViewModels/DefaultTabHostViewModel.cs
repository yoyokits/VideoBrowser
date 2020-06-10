namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    /// <summary>
    /// Defines the <see cref="DefaultTabHostViewModel" />.
    /// </summary>
    public class DefaultTabHostViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTabHostViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalBrowserData"/>.</param>
        public DefaultTabHostViewModel(GlobalBrowserData globalData)
        {
            this.WebBrowserTabControlViewModel = new WebBrowserTabControlViewModel(globalData);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the WebBrowserTabControlViewModel.
        /// </summary>
        public WebBrowserTabControlViewModel WebBrowserTabControlViewModel { get; }

        #endregion Properties
    }
}