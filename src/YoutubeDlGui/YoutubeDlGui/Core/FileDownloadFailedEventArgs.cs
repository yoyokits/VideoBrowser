namespace YoutubeDlGui.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="FileDownloadFailedEventArgs" />
    /// </summary>
    public class FileDownloadFailedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDownloadFailedEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/></param>
        /// <param name="fileDownload">The fileDownload<see cref="FileDownload"/></param>
        public FileDownloadFailedEventArgs(Exception exception, FileDownload fileDownload)
        {
            this.Exception = exception;
            this.FileDownload = fileDownload;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Exception
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the FileDownload
        /// </summary>
        public FileDownload FileDownload { get; private set; }

        #endregion Properties
    }
}