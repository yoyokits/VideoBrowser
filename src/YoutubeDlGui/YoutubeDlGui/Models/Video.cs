namespace YoutubeDlGui.Models
{
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;

    /// <summary>
    /// Defines the <see cref="Video" />
    /// </summary>
    public class Video : NotifyPropertyChanged
    {
        #region Fields

        private string _id;

        private string _title;

        private VideoState _videoState;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get => this._id; set => this.Set(this.PropertyChangedHandler, ref this._id, value); }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get => this._title; set => this.Set(this.PropertyChangedHandler, ref this._title, value); }

        /// <summary>
        /// Gets or sets the VideoState
        /// </summary>
        public VideoState VideoState { get => this._videoState; set => this.Set(this.PropertyChangedHandler, ref this._videoState, value); }

        #endregion Properties
    }
}