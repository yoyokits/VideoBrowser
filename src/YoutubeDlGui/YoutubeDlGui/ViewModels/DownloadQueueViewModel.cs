namespace YoutubeDlGui.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
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
            this.OperationCollectionView = CollectionViewSource.GetDefaultView(this.Operations);
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
        /// Gets the Operations.
        /// </summary>
        public ObservableCollection<OperationModel> Operations { get; } = new ObservableCollection<OperationModel>();

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
            var operationModel = new OperationModel(operation);
            this.Operations.Add(operationModel);
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