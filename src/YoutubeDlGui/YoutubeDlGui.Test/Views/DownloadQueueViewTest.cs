namespace YoutubeDlGui.Test.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.ObjectModel;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Test.Common;
    using YoutubeDlGui.Test.TestHelpers;
    using YoutubeDlGui.ViewModels;
    using YoutubeDlGui.Views;

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
            var viewModel = new DownloadQueueViewModel();
            this.CreateDummyOperations(viewModel.Operations);
            var view = new DownloadQueueView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        /// <summary>
        /// The CreateDummyOperations.
        /// </summary>
        /// <param name="operations">The operations<see cref="ObservableCollection{OperationModel}"/>.</param>
        private void CreateDummyOperations(ObservableCollection<OperationModel> operations)
        {
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                var operation = new DummyOperation();
                var operationModel = new OperationModel(operation);
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