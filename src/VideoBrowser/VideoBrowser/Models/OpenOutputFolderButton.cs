namespace VideoBrowser.Models
{
    using System.Diagnostics;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="OpenOutputFolderButton" />.
    /// </summary>
    public class OpenOutputFolderButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOutputFolderButton"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        public OpenOutputFolderButton(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.Command = new RelayCommand(this.OnOpenOutputFolder, nameof(OpenOutputFolderButton));
            this.Icon = Icons.Folder;
            this.ToolTip = "Open the output folder";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnOpenOutputFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnOpenOutputFolder(object obj)
        {
            Process.Start(this.GlobalData.Settings.OutputFolder);
        }

        #endregion Methods
    }
}