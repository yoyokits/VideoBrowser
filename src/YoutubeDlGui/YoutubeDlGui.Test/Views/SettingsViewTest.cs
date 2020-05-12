namespace YoutubeDlGui.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Test.Common;
    using YoutubeDlGui.Test.TestHelpers;
    using YoutubeDlGui.ViewModels;
    using YoutubeDlGui.Views;

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
            var globalData = new GlobalData();
            var viewModel = new SettingsViewModel(globalData);
            var view = new SettingsView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}