namespace YoutubeDlGui.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using YoutubeDlGui.Test.Common;
    using YoutubeDlGui.Test.TestHelpers;
    using YoutubeDlGui.ViewModels;
    using YoutubeDlGui.Views;

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