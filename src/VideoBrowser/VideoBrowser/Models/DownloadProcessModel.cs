namespace VideoBrowser.Models
{
    using CefSharp;

    /// <summary>
    /// Defines the <see cref="DownloadProcessModel" />.
    /// </summary>
    public class DownloadProcessModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadProcessModel"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="DownloadItem"/>.</param>
        public DownloadProcessModel(DownloadItem item)
        {
            this.DownloadItem = item;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadItem.
        /// </summary>
        internal DownloadItem DownloadItem { get; }

        #endregion Properties
    }
}