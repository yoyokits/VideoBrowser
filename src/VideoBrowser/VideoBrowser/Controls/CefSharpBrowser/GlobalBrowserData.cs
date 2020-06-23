namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="GlobalBrowserData" />.
    /// All singleton instances are saved here.
    /// </summary>
    public class GlobalBrowserData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalBrowserData"/> class.
        /// </summary>
        internal GlobalBrowserData()
        {
            this.Settings = new SettingsViewModel();
            this.InterTabClient = new InterTabClient(this);
            var settings = BrowserSettingsHelper.Load();
            if (settings != null)
            {
                this.BrowserSettings = settings;
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddInButtons.
        /// </summary>
        public ICollection<AddInButton> AddInButtons { get; } = new ObservableCollection<AddInButton>();

        /// <summary>
        /// Gets the BrowserSettings.
        /// </summary>
        public BrowserSettings BrowserSettings { get; } = new BrowserSettings();

        /// <summary>
        /// Gets the InterTabClient.
        /// </summary>
        public InterTabClient InterTabClient { get; }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings { get; }

        /// <summary>
        /// Gets or sets a value indicating whether IsSettingsLoaded.
        /// </summary>
        internal bool IsSettingsLoaded { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetAddInButton.
        /// </summary>
        /// <param name="addInType">The addInType<see cref="Type"/>.</param>
        /// <returns>The <see cref="AddInButton"/>.</returns>
        public AddInButton GetAddInButton(Type addInType)
        {
            var addIn = this.AddInButtons.FirstOrDefault(o => o.GetType() == addInType);
            return addIn;
        }

        #endregion Methods
    }
}