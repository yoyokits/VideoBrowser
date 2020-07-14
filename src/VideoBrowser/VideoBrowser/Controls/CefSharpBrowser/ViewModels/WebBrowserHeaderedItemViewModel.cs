namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media.Imaging;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Core;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="WebBrowserHeaderedItemViewModel" />.
    /// </summary>
    public class WebBrowserHeaderedItemViewModel : TabItem
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
        internal WebBrowserHeaderedItemViewModel(GlobalBrowserData globalBrowserData, CefWindowData cefWindowData, Func<Operation, string> downloadAction)
        {
            this.VideoBrowserViewModel = new VideoBrowserViewModel(globalBrowserData, cefWindowData)
            {
                DownloadAction = downloadAction
            };

            this.VideoBrowserViewModel.PropertyChanged += this.OnVideoBrowserViewModel_PropertyChanged;
            this.Title = this.VideoBrowserViewModel.Header;
            UIThreadHelper.Invoke(() =>
            {
                this.VideoBrowserView = new VideoBrowserView { DataContext = this.VideoBrowserViewModel };
                this.Content = this.VideoBrowserView;
            });
        }

        #endregion Constructors

        #region Properties

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
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.VideoBrowserViewModel == null)
            {
                return;
            }

            this.VideoBrowserViewModel.PropertyChanged -= this.OnVideoBrowserViewModel_PropertyChanged;
            this.VideoBrowserViewModel.Dispose();
            this.VideoBrowserViewModel = null;
            this.VideoBrowserView.DataContext = null;
            this.VideoBrowserView = null;
        }

        /// <summary>
        /// The OnVideoBrowserViewModel_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnVideoBrowserViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.VideoBrowserViewModel.FavIcon):
                    var favIcon = new BitmapImage();
                    favIcon.BeginInit();
                    favIcon.UriSource = new Uri(this.VideoBrowserViewModel.FavIcon);
                    favIcon.EndInit();
                    this.HeaderViewModel.Image = favIcon;
                    break;

                case nameof(this.VideoBrowserViewModel.Header):
                    this.Title = this.VideoBrowserViewModel.Header;
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}