namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="BrowserSettingsHelper" />.
    /// </summary>
    internal static class BrowserSettingsHelper
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="BrowserSettingsHelper"/> class.
        /// </summary>
        static BrowserSettingsHelper()
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            var userAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            UserJsonSettingsPath = Path.Combine(userAppDataPath, $@"{appName}\BrowserSettings.json");
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the UserJsonSettingsPath.
        /// </summary>
        public static string UserJsonSettingsPath { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Load.
        /// </summary>
        /// <returns>The <see cref="BrowserSettings"/>.</returns>
        internal static BrowserSettings Load()
        {
            Logger.Info($"Loading Browser setting {UserJsonSettingsPath}");
            if (!File.Exists(UserJsonSettingsPath))
            {
                return null;
            }

            try
            {
                using (StreamReader file = File.OpenText(UserJsonSettingsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var browserSettings = (BrowserSettings)serializer.Deserialize(file, typeof(BrowserSettings));
                    return browserSettings;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error Loading Browser Setting: {e.Message}");
            }

            return null;
        }

        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="settings">The settings<see cref="BrowserSettings"/>.</param>
        /// <param name="browserModel">The browserModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        internal static void Save(BrowserSettings settings, WebBrowserTabControlViewModel browserModel)
        {
            settings.TabSettingModels.Clear();
            settings.BookmarkModels.Clear();
            foreach (var tabItem in browserModel.TabItems)
            {
                if (tabItem.Content is FrameworkElement element && element.DataContext is VideoBrowserViewModel videoModel)
                {
                    var tabModel = new TabSettingsModel
                    {
                        Title = videoModel.Header,
                        Url = videoModel.Url
                    };

                    settings.TabSettingModels.Add(tabModel);
                }
            }

            settings.SelectedTabSettingIndex = browserModel.SelectedTabIndex;
            var settingsFolder = Path.GetDirectoryName(UserJsonSettingsPath);
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sw = new StreamWriter(UserJsonSettingsPath))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, settings);
            }

            Logger.Info($"Browser setting is saved in {UserJsonSettingsPath}");
        }

        #endregion Methods
    }
}