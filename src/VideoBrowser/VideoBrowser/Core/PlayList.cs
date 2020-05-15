namespace VideoBrowser.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PlayList" />
    /// </summary>
    public class PlayList
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayList"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="name">The name<see cref="string"/></param>
        /// <param name="onlineCount">The onlineCount<see cref="int"/></param>
        public PlayList(string id, string name, int onlineCount)
        {
            this.Id = id;
            this.Name = name;
            this.OnlineCount = onlineCount;
            this.Videos = new List<VideoInfo>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayList"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="name">The name<see cref="string"/></param>
        /// <param name="onlineCount">The onlineCount<see cref="int"/></param>
        /// <param name="videos">The videos<see cref="List{VideoInfo}"/></param>
        public PlayList(string id, string name, int onlineCount, List<VideoInfo> videos)
        {
            this.Id = id;
            this.Name = name;
            this.OnlineCount = onlineCount;
            this.Videos = videos;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the play list ID.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the playlist name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the OnlineCount
        /// Gets the expected video count. Expected because some videos might not be included because of errors.
        /// Look at 'Playlist.Videos' property for actual count.
        /// </summary>
        public int OnlineCount { get; set; }

        /// <summary>
        /// Gets the videos in the playlist. Videos with errors not included, for example country restrictions.
        /// </summary>
        public List<VideoInfo> Videos { get; private set; }

        #endregion Properties
    }
}