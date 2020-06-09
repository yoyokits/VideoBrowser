namespace VideoBrowserTestApp.Tests
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
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
            var globalBrowserData = new GlobalBrowserData();
            var viewModel = new WebBrowserTabControlViewModel(globalBrowserData);
            var cefWindowData = viewModel.CefWindowData;
            var view = new WebBrowserTabControlView { DataContext = viewModel };
            var window = WindowFactory.Create(view, testWindow);
            var operationModels = new ObservableCollection<OperationModel>();
            viewModel.TabItems.Add(new WebBrowserHeaderedItemViewModel(globalBrowserData, cefWindowData, null));
            cefWindowData.MainWindow = window; ;
            window.ShowDialog();
        }

        #endregion Methods
    }
}