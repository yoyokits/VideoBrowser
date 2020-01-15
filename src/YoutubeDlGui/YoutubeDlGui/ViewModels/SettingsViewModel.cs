namespace YoutubeDlGui.ViewModels
{
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="SettingsViewModel" />
    /// </summary>
    public class SettingsViewModel : NotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Settings;

        /// <summary>
        /// Gets or sets the OutputType
        /// </summary>
        public string OutputType { get; set; }

        #endregion Properties
    }
}