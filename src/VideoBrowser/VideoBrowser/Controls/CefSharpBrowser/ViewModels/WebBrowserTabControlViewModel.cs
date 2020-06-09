namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using VideoBrowser.Extensions;
    using VideoBrowser.Models;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="WebBrowserTabControlViewModel" />.
    /// </summary>
    public class WebBrowserTabControlViewModel : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserTabControlViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        public WebBrowserTabControlViewModel(GlobalBrowserData globalBrowserData)
        {
            this.GlobalBrowserData = globalBrowserData;
            this.CefWindowData = new CefWindowData();
            this.WebBrowsers = new ObservableCollection<HeaderedItemViewModel>();
            this.CreateBrowserFunc = this.CreateBrowser;
        }

        #endregion Constructors

        #region Properties

        public CefWindowData CefWindowData { get; }

        /// <summary>
        /// Gets the ClosingTabItemHandler.
        /// </summary>
        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        /// <summary>
        /// Gets the CreateBrowserFunc.
        /// </summary>
        public Func<HeaderedItemViewModel> CreateBrowserFunc { get; set; }

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        public GlobalBrowserData GlobalBrowserData { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.SearchVideo;

        /// <summary>
        /// Gets the InterTabClient.
        /// </summary>
        public IInterTabClient InterTabClient { get; }

        /// <summary>
        /// Gets the WebBrowsers.
        /// </summary>
        public ObservableCollection<HeaderedItemViewModel> WebBrowsers { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.WebBrowsers.ClearAndDispose();
        }

        /// <summary>
        /// Callback to handle tab closing.
        /// </summary>
        /// <param name="args">The args<see cref="ItemActionCallbackArgs{TabablzControl}"/>.</param>
        private static void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
        {
            //in here you can dispose stuff or cancel the close

            //here's your view model:
            var viewModel = args.DragablzItem.DataContext as HeaderedItemViewModel;

            //here's how you can cancel stuff:
            //args.Cancel();
            System.Diagnostics.Debug.Assert(viewModel != null);
        }

        /// <summary>
        /// The CreateBrowser.
        /// </summary>
        /// <returns>The <see cref="HeaderedItemViewModel"/>.</returns>
        private HeaderedItemViewModel CreateBrowser()
        {
            var model = new WebBrowserHeaderedItemViewModel(this.GlobalBrowserData, this.CefWindowData, null);
            return model;
        }

        #endregion Methods
    }
}