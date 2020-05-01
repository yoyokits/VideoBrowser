namespace YoutubeDlGui.Common
{
    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="Logging.Logger" />
    /// Log the important process to trace the last processes before error occurs.
    /// </summary>
    public static class Logger
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            Setup(nameof(YoutubeDlGui));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Log
        /// </summary>
        public static ILog Log { get; } = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the LogFilePath
        /// </summary>
        public static string LogFilePath { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Debug Log.
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Debug(this object source, object message)
        {
            Log.Debug(message);
        }

        /// <summary>
        /// The Error Log.
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Error(this object source, object message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// The Fatal Log.
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Fatal(this object source, object message)
        {
            Log.Fatal(message);
        }

        /// <summary>
        /// The Info
        /// </summary>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Info(object message = null)
        {
            if (message == null)
            {
                message = string.Empty;
            }

            Log.Info(message);
            System.Diagnostics.Trace.WriteLine(message);
        }

        /// <summary>
        /// The Info Log.
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Info(this object source, object message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// The Setup
        /// </summary>
        /// <param name="appName">The appName<see cref="string"/></param>
        public static void Setup(string appName)
        {
            LogFilePath = GetLogFilePath(appName);
            var hierarchy = (Hierarchy)LogManager.GetRepository(Assembly.GetEntryAssembly());

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
            };

            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = false,
                File = LogFilePath,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            var memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        /// <summary>
        /// The Warn
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="message">The message<see cref="object"/></param>
        public static void Warn(this object source, object message)
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Saves given Exception's stack trace to a readable file in the local application data folder.
        /// </summary>
        /// <param name="ex">The Exception to save.</param>
        public static void WriteException(Exception ex)
        {
            Logger.Info(ex.ToString());
        }

        /// <summary>
        /// The GetLogFilePath
        /// </summary>
        /// <param name="appName">The appName<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal static string GetLogFilePath(string appName)
        {
            var folder = AppEnvironment.GetUserLocalApplicationData();
            var path = Path.Combine(folder, $"{appName}.log");
            return path;
        }

        #endregion Methods
    }
}