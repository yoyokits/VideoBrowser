namespace VideoBrowserTestApp.Tests
{
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="DefaultTabHostWindowTest" />.
    /// </summary>
    public class DefaultTabHostWindowTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTabHostWindowTest"/> class.
        /// </summary>
        public DefaultTabHostWindowTest() : base(nameof(DefaultTabHostWindow))
        {
            this.GlobalBrowserData = new GlobalBrowserData();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        private GlobalBrowserData GlobalBrowserData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="testWindow">The testWindow<see cref="Window"/>.</param>
        protected override void Test(Window testWindow)
        {
            var client = this.GlobalBrowserData.InterTabClient;
            DefaultTabHostWindow window = null;
            UIThreadHelper.Invoke(() =>
            {
                var viewModel = new DefaultTabHostViewModel(this.GlobalBrowserData);
                var browserTabModel = viewModel.WebBrowserTabControlViewModel;
                var tab = new WebBrowserHeaderedItemViewModel(this.GlobalBrowserData, browserTabModel.CefWindowData, null);
                //browserTabModel.TabItems.Add(tab);
                window = new DefaultTabHostWindow { DataContext = viewModel };
            });

            window.ShowDialog();
        }

        #endregion Methods
    }
}