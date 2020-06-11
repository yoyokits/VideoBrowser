namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.Windows.Input;
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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserTabHeaderViewModel"/> class.
        /// </summary>
        public WebBrowserTabHeaderViewModel()
        {
            this.MouseUpCommand = new RelayCommand(this.OnMouseUp, nameof(this.MouseUpCommand));
        }

        #endregion Constructors

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

        /// <summary>
        /// Gets the MouseUpCommand.
        /// </summary>
        public ICommand MouseUpCommand { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnMouseUp.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnMouseUp(object obj)
        {
            Logger.Info($"Middle Mouse clicked on a browser tab to close it.");
            var (_, args, commandParameter) = (ValueTuple<object, EventArgs, object>)obj;
            if (args is MouseButtonEventArgs mouseEventArgs && mouseEventArgs.ChangedButton == MouseButton.Middle)
            {
                var dragablzItem = commandParameter as System.Windows.FrameworkElement;
                var closeCommand = TabablzControl.CloseItemCommand;
                if (closeCommand != null && closeCommand.CanExecute(commandParameter, dragablzItem))
                {
                    closeCommand.Execute(dragablzItem, dragablzItem);
                }
            }
        }

        #endregion Methods
    }
}