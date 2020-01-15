namespace YoutubeDlGui.ViewModels
{
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Models;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="VideoViewModel" />
    /// </summary>
    public class VideoViewModel : NotifyPropertyChanged
    {
        #region Fields

        private double _downloadProgress;

        private Video _video;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the DownloadProgress
        /// </summary>
        public double DownloadProgress { get => this._downloadProgress; set => this.Set(this.PropertyChangedHandler, ref this._downloadProgress, value); }

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon { get; set; } = Icons.SearchVideo;

        /// <summary>
        /// Gets or sets the Video
        /// </summary>
        public Video Video { get => this._video; set => this.Set(this.PropertyChangedHandler, ref this._video, value); }

        #endregion Properties
    }
}