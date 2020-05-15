namespace YoutubeDlGui.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="FileDownloadEventArgs" />
    /// </summary>
    public class FileDownloadEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDownloadEventArgs"/> class.
        /// </summary>
        /// <param name="fileDownload">The fileDownload<see cref="FileDownload"/></param>
        public FileDownloadEventArgs(FileDownload fileDownload) => this.FileDownload = fileDownload;

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the FileDownload
        /// </summary>
        public FileDownload FileDownload { get; private set; }

        #endregion Properties
    }
}