namespace YoutubeDlGui.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="DownloadVideoViewModel" />
    /// </summary>
    public class DownloadVideoViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadVideoViewModel"/> class.
        /// </summary>
        public DownloadVideoViewModel()
        {
            this.RemoveVideoCommand = new RelayCommand(this.OnRemoveVideo);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Download;

        /// <summary>
        /// Gets the RemoveVideoCommand
        /// </summary>
        public ICommand RemoveVideoCommand { get; }

        /// <summary>
        /// Gets the Videos
        /// </summary>
        public ObservableCollection<Video> Videos { get; } = new ObservableCollection<Video>();

        /// <summary>
        /// Gets the VideoViewSource
        /// </summary>
        public CollectionViewSource VideoViewSource { get; } = new CollectionViewSource();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnRemoveVideo
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        private void OnRemoveVideo(object obj)
        {
        }

        #endregion Methods
    }
}