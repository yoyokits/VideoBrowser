namespace VideoBrowser.Core
{
    /// <summary>
    /// Defines the <see cref="YoutubeUrlHandler" />
    /// </summary>
    public class YoutubeUrlHandler : UrlHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeUrlHandler"/> class.
        /// </summary>
        internal YoutubeUrlHandler() : base("youtube.com")
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The ParseFullUrl
        /// </summary>
        protected override void ParseFullUrl()
        {
            this.IsDownloadable = false;
            if (!this.FullUrl.Contains(this.DomainName))
            {
                return;
            }

            if (this.FullUrl.Contains(@"youtube.com/watch?v="))
            {
                this.IsDownloadable = true;
            }
        }

        #endregion Methods
    }
}