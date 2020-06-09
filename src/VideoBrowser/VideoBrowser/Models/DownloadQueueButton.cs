namespace VideoBrowser.Models
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
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
        /// <param name="operationModels">The operationModels<see cref="ObservableCollection{OperationModel}"/>.</param>
        internal DownloadQueueButton(ObservableCollection<OperationModel> operationModels) : base("Download", Icons.FolderDownload)
        {
            this.DownloadQueueViewModel = new DownloadQueueViewModel(operationModels);
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