namespace VideoBrowser.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Windows.Controls;
    using VideoBrowser.Core;
    using VideoBrowser.Models;
    using VideoBrowser.Test.Common;
    using VideoBrowser.Test.TestHelpers;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

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
            var viewModelA = new UrlEditorViewModel(urlReader, globalData) { IsVisible = true, FileName = "Youtube Video File Name", FileSize = "5 MB", Duration = "00:02:45" };
            var viewModelB = new UrlEditorViewModel(urlReader, globalData) { IsVisible = true, FileName = "Youtube Video File Name", FileSize = "1.4 MB", Duration = "00:02:45", IsBusy = true };
            var stackPanel = new StackPanel();
            stackPanel.Children.Add(new UrlEditorView { DataContext = viewModelA });
            stackPanel.Children.Add(new UrlEditorView { DataContext = viewModelB });
            WindowFactory.CreateAndShow(stackPanel);
        }

        #endregion Methods
    }
}