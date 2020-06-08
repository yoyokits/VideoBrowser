namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddInButtons.
        /// </summary>
        public ICollection<AddInButton> AddInButtons { get; } = new ObservableCollection<AddInButton>();

        #endregion Properties
    }
}