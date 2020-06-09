namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="GlobalBrowserData" />.
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
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddInButtons.
        /// </summary>
        public ICollection<AddInButton> AddInButtons { get; } = new ObservableCollection<AddInButton>();

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings { get; }

        #endregion Properties
    }
}