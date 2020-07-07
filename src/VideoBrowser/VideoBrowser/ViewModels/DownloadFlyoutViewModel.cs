namespace VideoBrowser.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="DownloadFlyoutViewModel" />.
    /// </summary>
    public class DownloadFlyoutViewModel : NotifyPropertyChanged
    {
        #region Fields

        private bool _isOpen;

        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFlyoutViewModel"/> class.
        /// </summary>
        /// <param name="models">The models<see cref="ObservableCollection{DownloadItemModel}"/>.</param>
        internal DownloadFlyoutViewModel(ObservableCollection<DownloadItemModel> models)
        {
            this.DownloadItemModels = models;
            this.DownloadItemModels.CollectionChanged += this.OnDownloadItemModels_CollectionChanged;
            this.ShowDownloadTabCommand = new RelayCommand(this.OnShowDownloadTab, nameof(this.ShowDownloadTabCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the DownloadItemModels.
        /// </summary>
        public ObservableCollection<DownloadItemModel> DownloadItemModels { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsOpen.
        /// </summary>
        public bool IsOpen { get => _isOpen; set => this.Set(this.PropertyChangedHandler, ref _isOpen, value); }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get => _message; internal set => this.Set(this.PropertyChangedHandler, ref _message, value); }

        /// <summary>
        /// Gets or sets the ShowDownloadTabAction.
        /// </summary>
        public Action ShowDownloadTabAction { get; set; }

        /// <summary>
        /// Gets the ShowDownloadTabCommand.
        /// </summary>
        public ICommand ShowDownloadTabCommand { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnDownloadItemModels_CollectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="NotifyCollectionChangedEventArgs"/>.</param>
        private void OnDownloadItemModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.IsOpen = true;
            }
        }

        /// <summary>
        /// The OnShowDownloadTab.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnShowDownloadTab(object obj)
        {
            this.ShowDownloadTabAction?.Invoke();
        }

        #endregion Methods
    }
}