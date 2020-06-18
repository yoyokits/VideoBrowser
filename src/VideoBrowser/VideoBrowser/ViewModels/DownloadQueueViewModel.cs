namespace VideoBrowser.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using VideoBrowser.Common;
    using VideoBrowser.Core;
    using VideoBrowser.Helpers;
    using VideoBrowser.Models;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="DownloadQueueViewModel" />.
    /// </summary>
    public class DownloadQueueViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadQueueViewModel"/> class.
        /// </summary>
        /// <param name="operationModels">The operationModels<see cref="ObservableCollection{OperationModel}"/>.</param>
        internal DownloadQueueViewModel(ObservableCollection<OperationModel> operationModels)
        {
            this.RemoveOperationCommand = new RelayCommand(this.OnRemoveOperation);
            this.OperationModels = operationModels;
            this.OperationCollectionView = CollectionViewSource.GetDefaultView(this.OperationModels);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Download;

        /// <summary>
        /// Gets the OperationCollectionView.
        /// </summary>
        public ICollectionView OperationCollectionView { get; }

        /// <summary>
        /// Gets the OperationModels.
        /// </summary>
        public ObservableCollection<OperationModel> OperationModels { get; }

        /// <summary>
        /// Gets the RemoveOperationCommand.
        /// </summary>
        public ICommand RemoveOperationCommand { get; }

        /// <summary>
        /// Gets or sets the ShowMessageAsync.
        /// </summary>
        public Action<string, string> ShowMessageAsync { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Download.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        public void Download(Operation operation)
        {
            var operationModel = new OperationModel(operation) { PauseDownloadAction = this.OnPauseDownloadCalled, CancelDownloadAction = this.OnCancelDownloadCalled };
            var element = (DispatcherObject)this.OperationCollectionView;
            element.InvokeAsync(() =>
            {
                if (!this.OperationModels.Contains(operationModel))
                {
                    this.OperationModels.Add(operationModel);
                    DownloadQueueHandler.Add(operation);
                }
                else
                {
                    var output = Path.GetFileName(operationModel.Operation.Output);
                    this.ShowMessageAsync("Download Canceled", $"The video/audio {output} is already downloaded");
                }
            });
        }

        /// <summary>
        /// The OnCancelDownloadCalled.
        /// </summary>
        /// <param name="model">The model<see cref="OperationModel"/>.</param>
        internal void OnCancelDownloadCalled(OperationModel model)
        {
            model.Dispose();
            this.OperationModels.Remove(model);
        }

        /// <summary>
        /// The OnPauseDownloadCalled.
        /// </summary>
        /// <param name="model">The model<see cref="OperationModel"/>.</param>
        internal void OnPauseDownloadCalled(OperationModel model)
        {
            if (model == null)
            {
                return;
            }

            var operation = model.Operation;
            var status = operation.Status;
            if (status != OperationStatus.Paused && status != OperationStatus.Queued && status != OperationStatus.Working)
            {
                return;
            }

            if (status == OperationStatus.Paused)
            {
                operation.Resume();
                model.PauseText = "Pause";
            }
            else
            {
                operation.Pause();
                model.PauseText = "Resume";
            }
        }

        /// <summary>
        /// The OnRemoveVideo.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnRemoveOperation(object obj)
        {
        }

        #endregion Methods
    }
}