namespace VideoBrowserTestApp.Tests
{
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;
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
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Test(object obj)
        {
            var globalData = new GlobalData();
            var viewModel = new WebBrowserTabControlViewModel(globalData);
            var view = new WebBrowserTabControlView { DataContext = viewModel };
            var window = WindowFactory.Create(view);
            globalData.MainWindow = window;
            window.ShowDialog();
        }

        #endregion Methods
    }
}