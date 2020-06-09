namespace VideoBrowser.Models
{
    using System.Diagnostics;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Resources;
    using VideoBrowser.ViewModels;

    /// <summary>
    /// Defines the <see cref="OpenOutputFolderButton" />.
    /// </summary>
    public class OpenOutputFolderButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOutputFolderButton"/> class.
        /// </summary>
        /// <param name="settings">The settings<see cref="SettingsViewModel"/>.</param>
        public OpenOutputFolderButton(SettingsViewModel settings)
        {
            this.SettingsViewModel = settings;
            this.Command = new RelayCommand(this.OnOpenOutputFolder, nameof(OpenOutputFolderButton));
            this.Icon = Icons.Folder;
            this.ToolTip = "Open the output folder";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the SettingsViewModel.
        /// </summary>
        public SettingsViewModel SettingsViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnOpenOutputFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnOpenOutputFolder(object obj)
        {
            Process.Start(this.SettingsViewModel.OutputFolder);
        }

        #endregion Methods
    }
}