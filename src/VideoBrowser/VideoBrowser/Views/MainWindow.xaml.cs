namespace VideoBrowser.Views
{
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
        /// This constructor is called once per application instance.
        /// </summary>
        public MainWindow() : this(new GlobalData(), new GlobalBrowserData())
        {
            var addIns = this.GlobalBrowserData.AddInButtons;
            addIns.Add(new OpenOutputFolderButton(this.GlobalBrowserData.Settings));
            if (DebugHelper.IsDebug)
            {
                addIns.Add(new TestButton());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// This constructor is intended to create new window after dragging the browser tab.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        internal MainWindow(GlobalData globalData, GlobalBrowserData globalBrowserData)
        {
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

        #endregion Methods
    }
}