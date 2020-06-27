namespace VideoBrowser.Models
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="GlobalData" />.
    /// </summary>
    public class GlobalData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        internal GlobalData()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the DownloadItemModels.
        /// </summary>
        public ObservableCollection<DownloadItemModel> DownloadItemModels { get; } = new ObservableCollection<DownloadItemModel>();

        /// <summary>
        /// Gets a value indicating whether IsDebug.
        /// </summary>
        public bool IsDebug => DebugHelper.IsDebug;

        /// <summary>
        /// Gets the TestCommand.
        /// </summary>
        public ICommand TestCommand { get; }

        #endregion Properties
    }
}