namespace YoutubeDlGui.Core
{
    using System;

    #region Enums

    /// <summary>
    /// Defines the Video Url Types based on the website specification.
    /// </summary>
    [Flags]
    public enum UrlTypes
    {
        /// <summary>
        /// Defines the None or the current URL is not complete or error.
        /// </summary>
        None = 0,

        /// <summary>
        /// Defines the URL is Video that can be downloaded.
        /// </summary>
        Video = 1,

        /// <summary>
        /// Defines the URL is a PlayList that has many videos.
        /// </summary>
        PlayList = 2
    }

    #endregion Enums
}