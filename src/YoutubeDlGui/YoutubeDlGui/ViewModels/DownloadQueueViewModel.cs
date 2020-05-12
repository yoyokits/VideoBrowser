namespace YoutubeDlGui.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="DownloadQueueViewModel" />.
    /// </summary>
    public class DownloadQueueViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadQueueViewModel"/> class.
        /// </summary>
        public DownloadQueueViewModel()
        {
            this.RemoveOperationCommand = new RelayCommand(this.OnRemoveOperation);
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
        public ObservableCollection<OperationModel> OperationModels { get; } = new ObservableCollection<OperationModel>();

        /// <summary>
        /// Gets the RemoveOperationCommand.
        /// </summary>
        public ICommand RemoveOperationCommand { get; }

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
            element.InvokeUIThread(() => this.OperationModels.Add(operationModel));
            DownloadQueueHandler.Add(operation);
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