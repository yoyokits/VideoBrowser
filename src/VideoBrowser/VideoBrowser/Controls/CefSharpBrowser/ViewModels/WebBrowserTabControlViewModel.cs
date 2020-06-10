namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using Dragablz.Dockablz;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="WebBrowserTabControlViewModel" />.
    /// </summary>
    public class WebBrowserTabControlViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private int _selectedTabIndex;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserTabControlViewModel"/> class.
        /// </summary>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        public WebBrowserTabControlViewModel(GlobalBrowserData globalBrowserData)
        {
            this.GlobalBrowserData = globalBrowserData;
            this.CefWindowData = new CefWindowData();
            this.TabItems = new ObservableCollection<TabItem>();
            this.CreateBrowserFunc = this.CreateBrowser;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the CefWindowData.
        /// </summary>
        public CefWindowData CefWindowData { get; }

        /// <summary>
        /// Gets the ClosingFloatingItemHandler.
        /// </summary>
        public ClosingFloatingItemCallback ClosingFloatingItemHandler => ClosingFloatingItemHandlerImpl;

        /// <summary>
        /// Gets the ClosingTabItemHandler.
        /// </summary>
        public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

        /// <summary>
        /// Gets or sets the CreateBrowserFunc.
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
        public IInterTabClient InterTabClient => this.GlobalBrowserData.InterTabClient;

        /// <summary>
        /// Gets or sets the SelectedTabIndex.
        /// </summary>
        public int SelectedTabIndex { get => _selectedTabIndex; set => this.Set(this.PropertyChanged, ref _selectedTabIndex, value); }

        /// <summary>
        /// Gets the TabItems.
        /// </summary>
        public ObservableCollection<TabItem> TabItems { get; }

        /// <summary>
        /// Gets the ToolItems.
        /// </summary>
        public ObservableCollection<HeaderedItemViewModel> ToolItems { get; } = new ObservableCollection<HeaderedItemViewModel>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The AddTab.
        /// </summary>
        /// <param name="item">The item<see cref="TabItem"/>.</param>
        public void AddTab(TabItem item)
        {
            if (this.IsTabItemExist(item.Guid))
            {
                this.SetActiveTab(item.Guid);
                Logger.Info($"Add tab canceled: Tab {item.Title} exists.");
                return;
            }

            this.TabItems.Add(item);
            Logger.Info($"Tab {item.Title} is added.");
            this.SelectedTabIndex = this.TabItems.Count - 1;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.TabItems.ClearAndDispose();
        }

        /// <summary>
        /// The IsTabItemExist.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTabItemExist(Guid guid)
        {
            return this.TabItems.Any((o) => o.Guid == guid) && guid != Guid.Empty;
        }

        /// <summary>
        /// The SetActiveTab.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        internal void SetActiveTab(Guid guid)
        {
            for (var i = 0; i < this.TabItems.Count; i++)
            {
                if (this.TabItems[i].Guid == guid)
                {
                    this.SelectedTabIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Callback to handle floating toolbar/MDI window closing.
        /// </summary>
        /// <param name="args">The args<see cref="ItemActionCallbackArgs{Layout}"/>.</param>
        private static void ClosingFloatingItemHandlerImpl(ItemActionCallbackArgs<Layout> args)
        {
            //in here you can dispose stuff or cancel the close

            //here's your view model:
            if (args.DragablzItem.DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
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
            if (viewModel.Content is FrameworkElement element && element.DataContext is IDisposable disposable)
            {
                disposable?.Dispose();
            }

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