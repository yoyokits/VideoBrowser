namespace YoutubeDlGui.ViewModels
{
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Models;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />
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
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadVideo
        /// Gets or sets the DownloadVideo
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel => this.GlobalData.DownloadQueueViewModel;

        /// <summary>
        /// Gets the GlobalData
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets the Settings
        /// </summary>
        public SettingsViewModel Settings => this.GlobalData.Settings;

        /// <summary>
        /// Gets the Title
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
        /// Gets the VideoBrowser
        /// </summary>
        public VideoBrowserViewModel VideoBrowser { get; } = new VideoBrowserViewModel();

        #endregion Properties
    }
}