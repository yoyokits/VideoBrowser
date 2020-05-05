namespace YoutubeDlGui.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Test.Common;
    using YoutubeDlGui.Test.TestHelpers;
    using YoutubeDlGui.ViewModels;
    using YoutubeDlGui.Views;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewTest" />.
    /// </summary>
    [TestClass]
    public class UrlEditorViewTest
    {
        #region Methods

        /// <summary>
        /// The Show_UrlEditorView.
        /// </summary>
        [TestMethod, ManualTest]
        public void Show_UrlEditorView()
        {
            var globalData = new GlobalData();
            var urlReader = new UrlReader();
            var viewModel = new UrlEditorViewModel(urlReader, globalData) { IsVisible = true };
            var view = new UrlEditorView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}