namespace VideoBrowser.Core
{
    /// <summary>
    /// Defines the <see cref="NoneUrlHandler" />
    /// </summary>
    public class NoneUrlHandler : UrlHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoneUrlHandler"/> class.
        /// </summary>
        public NoneUrlHandler() : base(string.Empty)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The ParseFullUrl
        /// </summary>
        protected override void ParseFullUrl()
        {
        }

        #endregion Methods
    }
}