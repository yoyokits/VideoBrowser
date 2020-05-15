namespace YoutubeDlGui.If
{
    using YoutubeDlGui.Core;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IUrlHandler" />
    /// </summary>
    public interface IUrlHandler
    {
        #region Properties

        /// <summary>
        /// Gets the main Domain Name like youtube.com.
        /// </summary>
        string DomainName { get; }

        /// <summary>
        /// Gets or sets the Full Url like youtube video or play list url.
        /// </summary>
        string FullUrl { get; set; }

        /// <summary>
        /// Gets a value indicating whether IsDownloadable
        /// </summary>
        bool IsDownloadable { get; }

        /// <summary>
        /// Gets a value indicating whether IsPlayList
        /// </summary>
        bool IsPlayList { get; }

        /// <summary>
        /// Gets the VideoUrlTypes
        /// </summary>
        UrlTypes VideoUrlTypes { get; }

        #endregion Properties
    }

    #endregion Interfaces
}