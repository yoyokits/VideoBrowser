namespace VideoBrowser.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="YoutubeAuthentication" />
    /// </summary>
    public class YoutubeAuthentication
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeAuthentication"/> class.
        /// </summary>
        /// <param name="username">The username<see cref="string"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        /// <param name="twoFactor">The twoFactor<see cref="string"/></param>
        public YoutubeAuthentication(string username, string password, string twoFactor)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception($"{this.GetType().Name}: {nameof(username)} and {nameof(password)} can't be empty or null.");
            }

            this.Username = username;
            this.Password = password;
            this.TwoFactor = twoFactor;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Password
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the TwoFactor
        /// </summary>
        public string TwoFactor { get; private set; }

        /// <summary>
        /// Gets the Username
        /// </summary>
        public string Username { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ToCmdArgument
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string ToCmdArgument()
        {
            var twoFactor = this.TwoFactor;
            if (!string.IsNullOrEmpty(twoFactor))
            {
                twoFactor = string.Format(Commands.TwoFactor, twoFactor);
            }

            return string.Format(Commands.Authentication,
                this.Username,
                this.Password) + twoFactor;
        }

        #endregion Methods
    }
}