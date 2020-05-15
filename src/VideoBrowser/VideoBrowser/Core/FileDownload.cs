namespace VideoBrowser.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="FileDownload" />
    /// </summary>
    public class FileDownload
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDownload"/> class.
        /// </summary>
        /// <param name="path">The path<see cref="string"/></param>
        /// <param name="url">The url<see cref="string"/></param>
        public FileDownload(string path, string url)
        {
            this.Path = path;
            this.Url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDownload"/> class.
        /// </summary>
        /// <param name="path">The path<see cref="string"/></param>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="alwaysCleanupOnCancel">The alwaysCleanupOnCancel<see cref="bool"/></param>
        public FileDownload(string path, string url, bool alwaysCleanupOnCancel)
            : this(path, url)
        {
            this.AlwaysCleanupOnCancel = alwaysCleanupOnCancel;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether AlwaysCleanupOnCancel
        /// </summary>
        public bool AlwaysCleanupOnCancel { get; set; }

        /// <summary>
        /// Gets the Directory
        /// </summary>
        public string Directory => System.IO.Path.GetDirectoryName(this.Path);

        /// <summary>
        /// Gets or sets the Exception
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsFinished
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Gets the Name
        /// </summary>
        public string Name => System.IO.Path.GetFileName(this.Path);

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the Progress
        /// </summary>
        public long Progress { get; set; }

        /// <summary>
        /// Gets or sets the TotalFileSize
        /// </summary>
        public long TotalFileSize { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        #endregion Properties
    }
}