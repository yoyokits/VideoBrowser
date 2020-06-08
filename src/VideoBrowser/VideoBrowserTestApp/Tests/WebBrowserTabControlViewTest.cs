namespace VideoBrowserTestApp.Tests
{
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Models;
    using VideoBrowserTestApp.Helpers;

    /// <summary>
    /// Defines the <see cref="WebBrowserTabControlViewTest" />.
    /// </summary>
    public class WebBrowserTabControlViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserTabControlViewTest"/> class.
        /// </summary>
        public WebBrowserTabControlViewTest() : base(nameof(WebBrowserTabControlView))
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="testWindow">The testWindow<see cref="Window"/>.</param>
        protected override void Test(Window testWindow)
        {
            var globalData = new GlobalData();
            var viewModel = new WebBrowserTabControlViewModel(globalData);
            var view = new WebBrowserTabControlView { DataContext = viewModel };
            var window = WindowFactory.Create(view, testWindow);
            viewModel.WebBrowsers.Add(new WebBrowserHeaderedItemViewModel(globalData));
            globalData.MainWindow = window;
            window.ShowDialog();
        }

        #endregion Methods
    }
}