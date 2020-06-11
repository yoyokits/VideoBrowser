namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="WebBrowserTabHeaderViewModel" />.
    /// </summary>
    public class WebBrowserTabHeaderViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string _header;

        private ImageSource _image;

        private bool _isSelected;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        public string Header { get => _header; set => this.Set(this.PropertyChangedHandler, ref _header, value); }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public ImageSource Image { get => _image; set => this.Set(this.PropertyChangedHandler, ref _image, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsSelected.
        /// </summary>
        public bool IsSelected { get => _isSelected; set => this.Set(this.PropertyChangedHandler, ref _isSelected, value); }

        #endregion Properties
    }
}