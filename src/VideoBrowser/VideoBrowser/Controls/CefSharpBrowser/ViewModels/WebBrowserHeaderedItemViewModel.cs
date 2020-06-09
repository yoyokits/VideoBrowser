namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.ComponentModel;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="WebBrowserHeaderedItemViewModel" />.
    /// </summary>
    public class WebBrowserHeaderedItemViewModel : HeaderedItemViewModel, IDisposable
    {
        #region Fields

        private VideoBrowserViewModel _videoBrowserViewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserHeaderedItemViewModel"/> class.
        /// </summary>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        /// <param name="cefWindowData">The cefWindowData<see cref="CefWindowData"/>.</param>
        /// <param name="downloadAction">The downloadAction<see cref="Action{Operation}"/>.</param>
        internal WebBrowserHeaderedItemViewModel(GlobalBrowserData globalBrowserData, CefWindowData cefWindowData, Action<Operation> downloadAction)
        {
            this.CefWindowData = cefWindowData;
            this.VideoBrowserViewModel = new VideoBrowserViewModel(globalBrowserData, this.CefWindowData);
            this.VideoBrowserViewModel.PropertyChanged += this.OnVideoBrowserViewModel_PropertyChanged;
            this.VideoBrowserViewModel.DownloadAction = downloadAction;

            this.HeaderViewModel = new WebBrowserTabHeaderViewModel { Header = this.VideoBrowserViewModel.Header };
            UIThreadHelper.Invoke(() =>
            {
                this.VideoBrowserView = new VideoBrowserView { DataContext = this.VideoBrowserViewModel };
                this.Header = new WebBrowserTabHeaderView { DataContext = this.HeaderViewModel };
            });
            this.Content = this.VideoBrowserView;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the CefWindowData.
        /// </summary>
        public CefWindowData CefWindowData { get; }

        /// <summary>
        /// Gets the HeaderViewModel.
        /// </summary>
        public WebBrowserTabHeaderViewModel HeaderViewModel { get; }

        /// <summary>
        /// Gets the VideoBrowserView.
        /// </summary>
        public VideoBrowserView VideoBrowserView { get; private set; }

        /// <summary>
        /// Gets the VideoBrowserViewModel.
        /// </summary>
        public VideoBrowserViewModel VideoBrowserViewModel { get => _videoBrowserViewModel; private set => this.Set(this.OnPropertyChanged, ref _videoBrowserViewModel, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.VideoBrowserViewModel == null)
            {
                return;
            }

            this.VideoBrowserViewModel.PropertyChanged -= this.OnVideoBrowserViewModel_PropertyChanged;
            this.VideoBrowserViewModel.Dispose();
            this.VideoBrowserViewModel = null;
            this.VideoBrowserView.DataContext = null;
            this.VideoBrowserView = null;
            this.Content = null;
            this.Header = null;
        }

        /// <summary>
        /// The OnVideoBrowserViewModel_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnVideoBrowserViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.IsMatch(nameof(this.VideoBrowserViewModel.Header)))
            {
                this.HeaderViewModel.Header = this.VideoBrowserViewModel.Header;
            }
        }

        #endregion Methods
    }
}