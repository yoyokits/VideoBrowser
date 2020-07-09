namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using VideoBrowser.Controls.CefSharpBrowser.Handlers;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Extensions;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="GlobalBrowserData" />.
    /// All singleton instances are saved here.
    /// </summary>
    public class GlobalBrowserData : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalBrowserData"/> class.
        /// </summary>
        internal GlobalBrowserData()
        {
            this.InterTabClient = new InterTabClient(this);
            this.Settings = new SettingsViewModel();
            this.Settings.PropertyChanged += this.OnSettings_PropertyChanged;
            this.DownloadHandler = new DownloadHandler(this.DownloadItemModels) { DownloadPath = this.Settings.OutputFolder };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AddInButtons.
        /// </summary>
        public ICollection<AddInButton> AddInButtons { get; } = new ObservableCollection<AddInButton>();

        /// <summary>
        /// Gets the BookmarkModels.
        /// </summary>
        public ObservableCollection<BookmarkModel> BookmarkModels => this.Settings.BookmarkModels;

        /// <summary>
        /// Gets the BrowserSettings.
        /// </summary>
        public BrowserSettings BrowserSettings => this.Settings.BrowserSettings;

        /// <summary>
        /// Gets the DownloadHandler.
        /// </summary>
        public DownloadHandler DownloadHandler { get; }

        /// <summary>
        /// Gets the DownloadItemModels.
        /// </summary>
        public ObservableCollection<DownloadItemModel> DownloadItemModels { get; } = new ObservableCollection<DownloadItemModel>();

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

        /// <summary>
        /// Gets the WindowViewModels.
        /// </summary>
        internal IList<MainWindowViewModel> WindowViewModels { get; } = new List<MainWindowViewModel>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.Settings.PropertyChanged -= this.OnSettings_PropertyChanged;
            this.DownloadHandler.Dispose();
        }

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

        /// <summary>
        /// The OnSettings_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.IsMatch(nameof(this.Settings.OutputFolder)) && Directory.Exists(this.Settings.OutputFolder))
            {
                this.DownloadHandler.DownloadPath = this.Settings.OutputFolder;
            }
        }

        #endregion Methods
    }
}