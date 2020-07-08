namespace VideoBrowser.Controls.CefSharpBrowser.AddIns
{
    using VideoBrowser.Controls.CefSharpBrowser.Resources;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="IsSecureAddIn" />.
    /// </summary>
    public class IsSecureAddIn : AddInButton
    {
        private string _url;

        public IsSecureAddIn()
        {
            this.Icon = BrowserIcons.Lock;
            this.ToolTip = "The connection is secure";
        }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url
        {
            get => _url;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _url, value) || string.IsNullOrEmpty(this.Url))
                {
                    return;
                }

                this.IsVisible = this.Url.Contains("https://");
            }
        }

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