namespace YoutubeDlGui.Views
{
    using System.Windows;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            Logger.Info($"Start {nameof(YoutubeDlGui)}");
            InitializeComponent();
            this.Loaded += (object sender, RoutedEventArgs e) => this.DataContext = new MainWindowViewModel();
        }

        #endregion Constructors
    }
}