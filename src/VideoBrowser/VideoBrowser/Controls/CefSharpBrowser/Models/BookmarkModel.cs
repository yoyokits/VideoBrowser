namespace VideoBrowser.Controls.CefSharpBrowser.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
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
            this.OpenCommand = new RelayCommand(this.OnOpen);
            this.RemoveCommand = new RelayCommand(this.OnRemove);
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
        /// Gets or sets the FavIcon.
        /// </summary>
        public string FavIcon { get => _favIcon; set => this.Set(this.PropertyChanged, ref _favIcon, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.Set(this.PropertyChanged, ref _name, value); }

        /// <summary>
        /// Gets or sets the OpenAction.
        /// </summary>
        [JsonIgnore]
        public Action<BookmarkModel, WebBrowserTabControlViewModel> OpenAction { get; internal set; }

        /// <summary>
        /// Gets the OpenCommand.
        /// </summary>
        [JsonIgnore]
        public ICommand OpenCommand { get; }

        /// <summary>
        /// Gets the RemoveCommand
        /// </summary>
        [JsonIgnore]
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        public string Url { get => _url; set => this.Set(this.PropertyChanged, ref _url, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnOpen.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnOpen(object obj)
        {
            var model = ((ValueTuple<object, EventArgs, object>)obj).Item3 as WebBrowserTabControlViewModel;
            model.OnOpenUrlFromTab(this.Url);
        }

        /// <summary>
        /// The OnRemove.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnRemove(object obj)
        {
            var model = ((ValueTuple<object, EventArgs, object>)obj).Item3 as WebBrowserTabControlViewModel;
            if (model == null || !model.GlobalBrowserData.BookmarkModels.Contains(this))
            {
                return;
            }

            model.GlobalBrowserData.BookmarkModels.Remove(this);
        }

        #endregion Methods
    }
}