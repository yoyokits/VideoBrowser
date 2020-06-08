namespace VideoBrowser.Views
{
    using System.Windows;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Extensions;
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
            addIns.Add(new OpenOutputFolderButton(this.GlobalData));
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
            this.MainWindowViewModel.GlobalData.PropertyChanged += this.OnGlobalData_PropertyChanged;
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
        private void OnGlobalData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsMatch(nameof(GlobalData.IsFullScreen)))
            {
                var globalData = this.MainWindowViewModel.GlobalData;
                if (globalData.IsFullScreen)
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