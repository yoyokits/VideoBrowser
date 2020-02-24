namespace YoutubeDlGui.Common
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="AppEnvironment" />
    /// </summary>
    public static class AppEnvironment
    {
        #region Constants

        public const string Name = "YouTube and Online Video Downloader";

        public const string ShortName = nameof(YoutubeDlGui);

        private const string JsonDirectory = "Json";

        private const string LogsDirectory = "Logs";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets the UserLocalApplicationData
        /// </summary>
        public static string UserLocalApplicationData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Gets the UserProfile
        /// </summary>
        public static string UserProfile { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the local app data directory for this program. Also makes sure the directory exists.
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public static string GetAppDataDirectory()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                AppEnvironment.ShortName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// The application folder
        /// </summary>
        /// <param name="specialFolder">The specialFolder<see cref="Environment.SpecialFolder"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetFolder(Environment.SpecialFolder specialFolder) => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Returns the json directory for this program. Also makes sure the directory exists.
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
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
        /// <returns>The <see cref="string"/></returns>
        public static string GetLogsDirectory()
        {
            var path = Path.Combine(GetAppDataDirectory(), LogsDirectory);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// The GetUserLocalApplicationData
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
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