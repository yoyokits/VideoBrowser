namespace VideoBrowserTestApp.Tests
{
    using System.Windows;
    using System.Windows.Controls;
    using VideoBrowser.Core;
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;
    using VideoBrowserTestApp.Helpers;

    /// <summary>
    /// Defines the <see cref="UrlEditorViewTest" />.
    /// </summary>
    public class UrlEditorViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlEditorViewTest"/> class.
        /// </summary>
        public UrlEditorViewTest() : base(nameof(UrlEditorView))
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
            var urlReader = new UrlReader();
            var settings = new SettingsViewModel();
            var viewModelA = new UrlEditorViewModel(urlReader, settings) { IsVisible = true, FileName = "Youtube Video File Name", FileSize = "5 MB", Duration = "00:02:45" };
            var viewModelB = new UrlEditorViewModel(urlReader, settings) { IsVisible = true, FileName = "Youtube Video File Name", FileSize = "1.4 MB", Duration = "00:02:45", IsBusy = true };
            var stackPanel = new StackPanel();
            stackPanel.Children.Add(new UrlEditorView { DataContext = viewModelA });
            stackPanel.Children.Add(new UrlEditorView { DataContext = viewModelB });
            WindowFactory.CreateAndShow(stackPanel, testWindow);
        }

        #endregion Methods
    }
}