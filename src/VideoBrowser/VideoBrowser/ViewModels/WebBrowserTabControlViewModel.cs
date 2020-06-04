namespace VideoBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
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
        public WebBrowserTabControlViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.WebBrowsers = new ObservableCollection<WebBrowserHeaderedItemViewModel>();
            this.AddBrowserCommand = new RelayCommand(this.OnAddBrowser, nameof(this.AddBrowserCommand));
            this.RemoveBrowserCommand = new RelayCommand(this.OnRemoveBrowser, nameof(this.RemoveBrowserCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddBrowserCommand.
        /// </summary>
        public ICommand AddBrowserCommand { get; }

        /// <summary>
        /// Gets the ClosingTabItemHandler.
        /// </summary>
        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.SearchVideo;

        /// <summary>
        /// Gets the InterTabClient.
        /// </summary>
        public IInterTabClient InterTabClient { get; }

        /// <summary>
        /// Gets the RemoveBrowserCommand.
        /// </summary>
        public ICommand RemoveBrowserCommand { get; }

        /// <summary>
        /// Gets the WebBrowsers.
        /// </summary>
        public ObservableCollection<WebBrowserHeaderedItemViewModel> WebBrowsers { get; }

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
        /// The Add.
        /// </summary>
        internal void Add()
        {
            var browser = new WebBrowserHeaderedItemViewModel(this.GlobalData)
            {
                Header = $"Site"
            };
            this.WebBrowsers.Add(browser);
        }

        internal void Remove(WebBrowserHeaderedItemViewModel item)
        {
            if (this.WebBrowsers.Count == 1 || item == null)
            {
                Logger.Warn(this, "Cannot remove video browser, there is only a single browser");
                return;
            }

            item.Dispose();
            this.WebBrowsers.Remove(item);
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
        /// The OnAddBrowser.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnAddBrowser(object obj)
        {
            this.Add();
        }

        /// <summary>
        /// The OnRemoveBrowser.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnRemoveBrowser(object obj)
        {
            this.Remove(obj as WebBrowserHeaderedItemViewModel);
        }

        #endregion Methods
    }
}