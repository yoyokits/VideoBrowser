namespace VideoBrowser.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="AppEnvironment" />.
    /// </summary>
    public static class AppEnvironment
    {
        #region Constants

        public const string Name = "Cekli Browser";

        public const string LongName = "Cekli Video Browser and Downloader";

        public const int ProgressUpdateDelay = 250;

        public const string ShortName = nameof(VideoBrowser);

        private const string BinariesDirectory = "Binaries";

        private const string JsonDirectory = "Json";

        private const string LogsDirectory = "Logs";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets the AppBinaryDirectory.
        /// </summary>
        public static string AppBinaryDirectory => Path.Combine(AppDirectory, BinariesDirectory);

        /// <summary>
        /// Gets the AppDirectory.
        /// </summary>
        public static string AppDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Gets or sets the Arguments.
        /// </summary>
        public static string[] Arguments { get; internal set; }

        /// <summary>
        /// Gets the Author.
        /// </summary>
        public static string Author => "Yohanes Wahyu Nurcahyo";

        /// <summary>
        /// Gets the HomeUrl.
        /// </summary>
        public static string HomeUrl => "https://yoyokits.github.io/VideoBrowser/welcome.html";

        /// <summary>
        /// Gets the ProjectUrl.
        /// </summary>
        public static string ProjectUrl { get; } = "https://github.com/yoyokits/VideoBrowser";

        /// <summary>
        /// Gets the UserLocalApplicationData.
        /// </summary>
        public static string UserLocalApplicationData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Gets the UserProfile.
        /// </summary>
        public static string UserProfile { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Gets the UserVideoFolder.
        /// </summary>
        public static string UserVideoFolder { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public static string Version { get; } = $"{Assembly.GetExecutingAssembly().GetName().Version.ToString()} - Alpha";

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the local app data directory for this program. Also makes sure the directory exists.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetAppDataDirectory()
        {
            var path = Path.Combine(UserLocalApplicationData, AppEnvironment.ShortName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// The application folder.
        /// </summary>
        /// <param name="specialFolder">The specialFolder<see cref="Environment.SpecialFolder"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetFolder(Environment.SpecialFolder specialFolder) => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Returns the json directory for this program. Also makes sure the directory exists.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetJsonDirectory()
        {
            var path = Path.Combine(GetAppDataDirectory(), JsonDirectory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Returns the logs directory for this program. Also makes sure the directory exists.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLogsDirectory()
        {
            var path = Path.Combine(GetAppDataDirectory(), LogsDirectory);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// The GetUserLocalApplicationData.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetUserLocalApplicationData()
        {
            var userFolder = UserLocalApplicationData;
            var appFolder = Path.Combine(userFolder, ShortName);
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return appFolder;
        }

        #endregion Methods
    }
}