namespace VideoBrowser.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VideoBrowser.Test.Common;
    using VideoBrowser.Test.TestHelpers;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="AboutViewTest" />.
    /// </summary>
    [TestClass]
    public class AboutViewTest
    {
        #region Methods

        /// <summary>
        /// The Show_AboutView.
        /// </summary>
        [TestMethod, ManualTest]
        public void Show_AboutView()
        {
            var viewModel = new AboutViewModel();
            var view = new AboutView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}