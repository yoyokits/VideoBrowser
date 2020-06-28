namespace VideoBrowser.Models
{
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
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
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected override void Execute(WebBrowserTabControlViewModel viewModel)
        {
            ProcessHelper.OpenFolder(this.SettingsViewModel.OutputFolder);
        }

        #endregion Methods
    }
}