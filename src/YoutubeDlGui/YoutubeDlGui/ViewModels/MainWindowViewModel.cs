namespace YoutubeDlGui.ViewModels
{
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />
    /// </summary>
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets the DownloadVideo
        /// Gets or sets the DownloadVideo
        /// </summary>
        public DownloadQueueViewModel DownloadVideo { get; } = new DownloadQueueViewModel();

        /// <summary>
        /// Gets the Settings
        /// </summary>
        public SettingsViewModel Settings { get; } = new SettingsViewModel();

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