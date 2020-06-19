namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using System.ComponentModel;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="TabSettingsModel" />.
    /// </summary>
    public class TabSettingsModel : INotifyPropertyChanged
    {
        #region Fields

        private string _url;

        #endregion Fields

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => _url; set => this.Set(this.PropertyChanged, ref _url, value); }

        #endregion Properties
    }
}