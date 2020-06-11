namespace VideoBrowser.Views
{
    using Dragablz;
    using System.Windows;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() : this(new GlobalData(), new GlobalBrowserData())
        {
            // This constructor is called once per application instance.
            var addIns = this.GlobalBrowserData.AddInButtons;
            addIns.Add(new DownloadQueueButton(this.GlobalData.OperationModels));
            addIns.Add(new OpenOutputFolderButton(this.GlobalBrowserData.Settings));
            addIns.Add(new SettingsButton(this.GlobalBrowserData.Settings));
            addIns.Add(new AboutButton());
            if (DebugHelper.IsDebug)
            {
                addIns.Add(new TestButton());
            }

            // Register create browser tab.
            this.GlobalBrowserData.InterTabClient.CreateWindow = this.CreateWindow;

            // Add the first browser tab.
            var browserTabModel = this.MainWindowViewModel.WebBrowserTabControlViewModel;
            browserTabModel.TabItems.Add(this.MainWindowViewModel.CreateBrowser());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        internal MainWindow(GlobalData globalData, GlobalBrowserData globalBrowserData)
        {
            // This constructor is intended to create new window after dragging the browser tab.
            Logger.Info($"Start {nameof(VideoBrowser)}");
            this.GlobalData = globalData;
            this.GlobalBrowserData = globalBrowserData;
            this.MainWindowViewModel = new MainWindowViewModel(globalData, globalBrowserData);
            this.MainWindowViewModel.CefWindowData.PropertyChanged += this.CefWindowData_PropertyChanged;
            this.DataContext = this.MainWindowViewModel;
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        internal GlobalBrowserData GlobalBrowserData { get; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        internal GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the LastWindowState.
        /// </summary>
        private WindowState LastWindowState { get; set; }

        /// <summary>
        /// Gets or sets the LastWindowStyle.
        /// </summary>
        private WindowStyle LastWindowStyle { get; set; }

        /// <summary>
        /// Gets or sets the MainWindowViewModel.
        /// </summary>
        private MainWindowViewModel MainWindowViewModel { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnGlobalData_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void CefWindowData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var cefWindowData = this.MainWindowViewModel.CefWindowData;
            if (e.IsMatch(nameof(cefWindowData.IsFullScreen)))
            {
                if (cefWindowData.IsFullScreen)
                {
                    this.LastWindowState = this.WindowState;
                    this.LastWindowStyle = this.WindowStyle;
                    this.WindowState = WindowState.Maximized;
                    this.WindowStyle = WindowStyle.None;
                    this.ShowTitleBar = false;
                }
                else
                {
                    this.WindowState = this.LastWindowState;
                    this.WindowStyle = this.LastWindowStyle;
                    this.ShowTitleBar = true;
                }
            }
        }

        /// <summary>
        /// The CreateWindow.
        /// </summary>
        /// <returns>The <see cref="(Window, TabablzControl)"/>.</returns>
        private (Window, TabablzControl) CreateWindow()
        {
            var viewModel = new MainWindowViewModel(this.GlobalData, this.GlobalBrowserData);
            var window = new MainWindow(this.GlobalData, this.GlobalBrowserData) { DataContext = viewModel };
            var initialTabablzControl = window.WebBrowserTabControlView.InitialTabablzControl;
            return (window, initialTabablzControl);
        }

        #endregion Methods
    }
}