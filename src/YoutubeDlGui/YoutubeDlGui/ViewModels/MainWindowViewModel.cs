namespace YoutubeDlGui.ViewModels
{
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />
    /// </summary>
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string _outputFolder;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the DownloadVideo
        /// Gets or sets the DownloadVideo
        /// </summary>
        public DownloadVideoViewModel DownloadVideo { get; } = new DownloadVideoViewModel();

        /// <summary>
        /// Gets or sets the OuputFolder
        /// </summary>
        public string OuputFolder { get => this._outputFolder; set => this.Set(this.PropertyChangedHandler, ref this._outputFolder, value); }

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