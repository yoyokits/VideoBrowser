namespace VideoBrowser.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using VideoBrowser.Core;
    using VideoBrowser.Models;
    using VideoBrowser.Test.Common;
    using VideoBrowser.Test.TestHelpers;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="DownloadQueueViewTest" />.
    /// </summary>
    [TestClass]
    public class DownloadQueueViewTest
    {
        #region Methods

        /// <summary>
        /// The Show_DownloadQueueView.
        /// </summary>
        [TestMethod, ManualTest]
        public void Show_DownloadQueueView()
        {
            var globalData = new GlobalData();
            var viewModel = new DownloadQueueViewModel(globalData.OperationModels);
            this.CreateDummyOperations(viewModel.OperationModels, viewModel.OnPauseDownloadCalled);
            var view = new DownloadQueueView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        /// <summary>
        /// The CreateDummyOperations.
        /// </summary>
        /// <param name="operations">The operations<see cref="ObservableCollection{OperationModel}"/>.</param>
        /// <param name="pauseDownloadAction">The pauseDownloadAction<see cref="ICommand"/>.</param>
        private void CreateDummyOperations(ObservableCollection<OperationModel> operations, Action<OperationModel> pauseDownloadAction)
        {
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                var operation = new DummyOperation() { Status = (OperationStatus)(i % 6) };
                var operationModel = new OperationModel(operation) { PauseDownloadAction = pauseDownloadAction, IsQueuedControlsVisible = (i & 1) == 1 };
                var progress = TestHelper.GetRandomLong(0, 100);
                operation.Duration = TestHelper.GetRandomLong(10, 10000);
                operation.FileSize = TestHelper.GetRandomLong(10000, 10000000000);
                operation.Input = $"https://youtube.com/view={i}";
                operation.Link = $"https://youtube.com/link={i}"; ;
                operation.Output = $@"C:\Users\YoutubeUser\File{TestHelper.GetRandomLong(100, 10000)}.mp4";
                operation.Progress = progress;
                operation.ProgressPercentage = (int)progress;
                operation.ProgressText = $"{progress}%";
                operation.ReportsProgress = true;
                operation.Speed = $"{TestHelper.GetRandomInt(10, 100)}MB/s";
                operation.Status = (OperationStatus)(i % 6);
                operation.Thumbnail = null;
                operation.Title = $"Youtube Title Movie Number {i}";
                operations.Add(operationModel);
            }
        }

        #endregion Methods
    }
}