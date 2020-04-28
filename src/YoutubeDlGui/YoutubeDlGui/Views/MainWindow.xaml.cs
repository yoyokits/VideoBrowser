namespace YoutubeDlGui.Views
{
    using YoutubeDlGui.Common;
    using YoutubeDlGui.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        #endregion Constructors
    }
}