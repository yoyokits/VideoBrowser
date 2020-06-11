namespace VideoBrowser.Models
{
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="TestButton" />.
    /// </summary>
    public class TestButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestButton"/> class.
        /// </summary>
        public TestButton()
        {
            this.Icon = Icons.Test;
            this.ToolTip = "Test a code";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected override void Execute(WebBrowserTabControlViewModel viewModel)
        {
            // Write the code to test here.
        }

        #endregion Methods
    }
}