namespace YoutubeDlGui.Models
{
    using YoutubeDlGui.ViewModels;

    /// <summary>
    /// Defines the <see cref="GlobalData" />
    /// </summary>
    public class GlobalData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        internal GlobalData()
        {
            this.DownloadQueueViewModel = new DownloadQueueViewModel();
            this.Settings = new SettingsViewModel();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadQueueViewModel
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel { get; }

        /// <summary>
        /// Gets the Settings
        /// </summary>
        public SettingsViewModel Settings { get; }

        #endregion Properties
    }
}