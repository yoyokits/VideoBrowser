namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using Dragablz.Dockablz;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="WebBrowserTabControlViewModel" />.
    /// </summary>
    public class WebBrowserTabControlViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private static int IdCounter;

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
            this.TabItems.CollectionChanged += this.OnTabItems_CollectionChanged;
            this.CreateBrowserFunc = this.CreateBrowser;
            this.CefWindowData.CefRequestHandler.OpenUrlFromTabAction = this.OnOpenUrlFromTab;
            this.CefWindowData.CefContextMenuHandler.OpenInNewTabAction = this.OnOpenUrlFromTab;
            this.CefWindowData.CefContextMenuHandler.OpenInNewWindowAction = this.OnOpenUrlFromWindow;
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
        /// Gets the Id.
        /// </summary>
        public int Id { get; } = IdCounter++;

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
            this.TabItems.CollectionChanged -= this.OnTabItems_CollectionChanged;
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"{nameof(WebBrowserTabControlViewModel)} {this.Id}";
            return message;
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

        /// <summary>
        /// The OnOpenUrlFromTab.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        private void OnOpenUrlFromTab(string url)
        {
            var browser = this.CreateBrowserFunc() as WebBrowserHeaderedItemViewModel;
            browser.VideoBrowserViewModel.NavigateUrl = url;
            UIThreadHelper.InvokeAsync(() => this.AddTab(browser));
        }

        /// <summary>
        /// The OnOpenUrlFromWindow.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        private void OnOpenUrlFromWindow(string url)
        {
            var browser = this.CreateBrowserFunc() as WebBrowserHeaderedItemViewModel;
            browser.VideoBrowserViewModel.NavigateUrl = url;
            var (Window, TabablzControl) = this.GlobalBrowserData.InterTabClient.CreateWindow();
            UIThreadHelper.Invoke(() =>
            {
                if (TabablzControl.ItemsSource is ICollection<TabItem> items)
                {
                    items.Add(browser);
                }

                Window.Show();
            });
        }

        /// <summary>
        /// The OnTabItems_CollectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="NotifyCollectionChangedEventArgs"/>.</param>
        private void OnTabItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is WebBrowserHeaderedItemViewModel browserTabItem)
                    {
                        browserTabItem.VideoBrowserViewModel.CefWindowData = this.CefWindowData;
                    }
                }
            }
        }

        #endregion Methods
    }
}