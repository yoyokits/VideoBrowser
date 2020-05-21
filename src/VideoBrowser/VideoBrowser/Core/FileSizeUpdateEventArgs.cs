namespace VideoBrowser.Core
{
    using System;

    #region Delegates

    /// <summary>
    /// The FileSizeUpdateHandler
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="FileSizeUpdateEventArgs"/></param>
    public delegate void FileSizeUpdateHandler(object sender, FileSizeUpdateEventArgs e);

    #endregion Delegates

    /// <summary>
    /// Defines the <see cref="FileSizeUpdateEventArgs" />
    /// </summary>
    public class FileSizeUpdateEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSizeUpdateEventArgs"/> class.
        /// </summary>
        /// <param name="videoFormat">The videoFormat<see cref="VideoFormat"/></param>
        public FileSizeUpdateEventArgs(VideoFormat videoFormat)
        {
            this.VideoFormat = videoFormat;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the VideoFormat
        /// </summary>
        public VideoFormat VideoFormat { get; set; }

        #endregion Properties
    }
}