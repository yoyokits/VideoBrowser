﻿namespace VideoBrowserTestApp.Tests
{
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;
    using VideoBrowserTestApp.Helpers;

    /// <summary>
    /// Defines the <see cref="DownloadQueueViewTest" />.
    /// </summary>
    public class DownloadQueueViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadQueueViewTest"/> class.
        /// </summary>
        public DownloadQueueViewTest() : base(nameof(DownloadQueueView))
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
            var globalBrowserData = new GlobalBrowserData();
            var viewModel = new DownloadQueueViewModel(globalBrowserData.DownloadItemModels);
            var view = new DownloadQueueView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view, testWindow);
        }

        #endregion Methods
    }
}