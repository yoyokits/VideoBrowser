namespace YoutubeDlGui.ViewModels
{
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Extensions;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="SettingsViewModel" />
    /// </summary>
    public class SettingsViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string _outputFolder;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Settings;

        /// <summary>
        /// Gets or sets the OuputFolder
        /// </summary>
        public string OuputFolder { get => this._outputFolder; set => this.Set(this.PropertyChangedHandler, ref this._outputFolder, value); }

        /// <summary>
        /// Gets or sets the OutputType
        /// </summary>
        public string OutputType { get; set; }

        #endregion Properties
    }
}