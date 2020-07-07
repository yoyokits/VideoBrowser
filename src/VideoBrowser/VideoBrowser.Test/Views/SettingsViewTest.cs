namespace VideoBrowser.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Test.Common;
    using VideoBrowser.Test.TestHelpers;

    /// <summary>
    /// Defines the <see cref="SettingsViewTest" />.
    /// </summary>
    [TestClass]
    public class SettingsViewTest
    {
        #region Methods

        /// <summary>
        /// The Show_SettingsView.
        /// </summary>
        [TestMethod, ManualTest]
        public void Show_SettingsView()
        {
            var viewModel = new SettingsViewModel();
            var view = new SettingsView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}