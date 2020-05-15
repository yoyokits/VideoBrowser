namespace YoutubeDlGui.Views
{
    using System.Windows;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            Logger.Info($"Start {nameof(YoutubeDlGui)}");
            this.MainWindowViewModel = new MainWindowViewModel();
            this.MainWindowViewModel.GlobalData.PropertyChanged += this.OnGlobalData_PropertyChanged;
            this.DataContext = this.MainWindowViewModel;
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the LastWindowState.
        /// </summary>
        private WindowState LastWindowState { get; set; }

        /// <summary>
        /// Gets or sets the LastWindowStyle.
        /// </summary>
        private WindowStyle LastWindowStyle { get; set; }

        /// <summary>
        /// Gets the MainWindowViewModel.
        /// </summary>
        private MainWindowViewModel MainWindowViewModel { get; }

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