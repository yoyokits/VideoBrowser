namespace YoutubeDlGui.ViewModels
{
    using System.Windows;
    using System.Windows.Input;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Core;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Views;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.GlobalData = new GlobalData();
            this.LoadedCommand = new RelayCommand(this.OnLoaded);
            this.ClosingCommand = new RelayCommand(this.OnClosing);
            this.VideoBrowser = new VideoBrowserViewModel(this.GlobalData.DownloadQueueViewModel.Download);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ClosingCommand.
        /// </summary>
        public RelayCommand ClosingCommand { get; }

        /// <summary>
        /// Gets the DownloadVideo
        /// Gets or sets the DownloadVideo....
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel => this.GlobalData.DownloadQueueViewModel;

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets the LoadedCommand.
        /// </summary>
        public ICommand LoadedCommand { get; }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings => this.GlobalData.Settings;

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                var title = "Youtube and Online Video Downloader v0.1";
                return title;
            }
        }

        /// <summary>
        /// Gets the VideoBrowser.
        /// </summary>
        public VideoBrowserViewModel VideoBrowser { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        private void Dispose()
        {
            this.VideoBrowser.Dispose();
        }

        /// <summary>
        /// The OnClosing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClosing(object obj)
        {
            var window = (MainWindow)obj;
            var settings = Properties.Settings.Default;
            settings.WindowPosition = new Point(window.Left, window.Top);
            settings.WindowWidth = window.ActualWidth;
            settings.WindowHeight = window.Height;
            settings.Save();
            DownloadQueueHandler.Stop();
            this.Dispose();
        }

        /// <summary>
        /// The OnLoaded.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLoaded(object obj)
        {
            var settings = Properties.Settings.Default;
            var window = (MainWindow)obj;
            window.Left = settings.WindowPosition.X;
            window.Top = settings.WindowPosition.Y;
            window.Width = settings.WindowWidth;
            window.Height = settings.WindowHeight;
            DownloadQueueHandler.LimitDownloads = settings.ShowMaxSimDownloads;
            DownloadQueueHandler.StartWatching(settings.MaxSimDownloads);
        }

        #endregion Methods
    }
}