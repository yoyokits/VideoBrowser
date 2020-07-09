namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Windows.Input;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="BookmarkModel" />.
    /// </summary>
    public class BookmarkModel : INotifyPropertyChanged
    {
        #region Fields

        private string _favIcon;

        private string _name;

        private string _url;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarkModel"/> class.
        /// </summary>
        internal BookmarkModel()
        {
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the ClickCommand.
        /// </summary>
        [JsonIgnore]
        public ICommand ClickCommand { get; internal set; }

        /// <summary>
        /// Gets or sets the FavIcon.
        /// </summary>
        public string FavIcon { get => _favIcon; set => this.Set(this.PropertyChanged, ref _favIcon, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.Set(this.PropertyChanged, ref _name, value); }

        /// <summary>
        /// Gets or sets the RemoveCommand.
        /// </summary>
        [JsonIgnore]
        public ICommand RemoveCommand { get; internal set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => _url; set => this.Set(this.PropertyChanged, ref _url, value); }

        #endregion Properties
    }
}