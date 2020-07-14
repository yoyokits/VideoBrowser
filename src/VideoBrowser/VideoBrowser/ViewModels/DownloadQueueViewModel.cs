namespace VideoBrowser.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
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
        /// <param name="downloadModels">The downloadModels<see cref="IList{DownloadItemModel}"/>.</param>
        internal DownloadQueueViewModel(IList<DownloadItemModel> downloadModels)
        {
            this.DownloadItemModels = downloadModels;
            this.OperationCollectionView = CollectionViewSource.GetDefaultView(this.DownloadItemModels);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadItemModels.
        /// </summary>
        public IList<DownloadItemModel> DownloadItemModels { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Download;

        /// <summary>
        /// Gets the OperationCollectionView.
        /// </summary>
        public ICollectionView OperationCollectionView { get; }

        /// <summary>
        /// Gets the RemoveOperationCommand.
        /// </summary>
        public ICommand RemoveOperationCommand { get; }

        #endregion Properties
    }
}