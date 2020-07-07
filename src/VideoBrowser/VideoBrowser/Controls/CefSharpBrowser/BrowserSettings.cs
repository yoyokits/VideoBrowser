namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.Collections.Generic;
    using VideoBrowser.Controls.CefSharpBrowser.Models;

    /// <summary>
    /// Defines the <see cref="BrowserSettings" />.
    /// </summary>
    public class BrowserSettings
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserSettings"/> class.
        /// </summary>
        public BrowserSettings()
        {
            this.BookmarkModels = new List<BookmarkModel>();
            this.TabSettingModels = new List<TabSettingsModel>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BookmarkModels.
        /// </summary>
        public IList<BookmarkModel> BookmarkModels { get; set; }

        /// <summary>
        /// Gets or sets the SelectedTabSettingIndex.
        /// </summary>
        public int SelectedTabSettingIndex { get; set; }

        /// <summary>
        /// Gets or sets the TabSettingModels.
        /// </summary>
        public IList<TabSettingsModel> TabSettingModels { get; set; }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        public string Version { get; set; } = "1.0";

        #endregion Properties
    }
}