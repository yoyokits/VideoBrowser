namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using System.ComponentModel;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="BookmarkModel" />.
    /// </summary>
    public class BookmarkModel : INotifyPropertyChanged
    {
        #region Fields

        private string _name;

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
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.Set(this.PropertyChanged, ref _name, value); }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => _url; set => this.Set(this.PropertyChanged, ref _url, value); }

        #endregion Properties
    }
}