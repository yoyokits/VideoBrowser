namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.ComponentModel;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

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
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal WebBrowserHeaderedItemViewModel(GlobalData globalData)
        {
            this.VideoBrowserViewModel = new VideoBrowserViewModel(globalData);
            this.VideoBrowserViewModel.PropertyChanged += this.OnVideoBrowserViewModel_PropertyChanged;
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