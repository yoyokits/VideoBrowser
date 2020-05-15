namespace VideoBrowser.Core
{
    using System.Collections.Generic;
    using VideoBrowser.If;

    /// <summary>
    /// Defines the <see cref="UrlHandlerProvider" />
    /// </summary>
    public class UrlHandlerProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlHandlerProvider"/> class.
        /// </summary>
        public UrlHandlerProvider()
        {
            this.UrlHandlerDict = new Dictionary<string, IUrlHandler>();
            this.AddHandler(new YoutubeUrlHandler());
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the NoneUrlHandler
        /// </summary>
        internal static IUrlHandler NoneUrlHandler { get; } = new NoneUrlHandler();

        /// <summary>
        /// Gets the UrlHandlerDict
        /// </summary>
        private IDictionary<string, IUrlHandler> UrlHandlerDict { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetUrlHandler
        /// </summary>
        /// <param name="url">The url<see cref="string"/></param>
        /// <returns>The <see cref="IUrlHandler"/></returns>
        internal IUrlHandler GetUrlHandler(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            foreach (var handlerPair in this.UrlHandlerDict)
            {
                if (url.Contains(handlerPair.Key))
                {
                    return handlerPair.Value;
                }
            }

            return NoneUrlHandler;
        }

        /// <summary>
        /// The AddHandler
        /// </summary>
        /// <param name="handler">The handler<see cref="IUrlHandler"/></param>
        private void AddHandler(IUrlHandler handler)
        {
            this.UrlHandlerDict.Add(handler.DomainName, handler);
        }

        #endregion Methods
    }
}