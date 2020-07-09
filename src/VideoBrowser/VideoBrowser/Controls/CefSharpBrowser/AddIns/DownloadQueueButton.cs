namespace VideoBrowser.Models
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Resources;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="DownloadQueueButton" />.
    /// </summary>
    internal class DownloadQueueButton : CreateTabAddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadQueueButton"/> class.
        /// </summary>
        /// <param name="downloadItemModels">The downloadItemModels<see cref="ObservableCollection{DownloadItemModel}"/>.</param>
        internal DownloadQueueButton(ObservableCollection<DownloadItemModel> downloadItemModels) : base("Downloads", Icons.FolderDownload)
        {
            this.DownloadQueueViewModel = new DownloadQueueViewModel(downloadItemModels);
            this.ToolTip = "Show downloads queue";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadQueueViewModel.
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateView.
        /// </summary>
        /// <returns>The <see cref="UIElement"/>.</returns>
        protected override UIElement CreateView()
        {
            var downloadQueueView = new DownloadQueueView { DataContext = this.DownloadQueueViewModel };
            return downloadQueueView;
        }

        #endregion Methods
    }
}