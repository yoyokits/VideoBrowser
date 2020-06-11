namespace VideoBrowser.Controls.CefSharpBrowser
{
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="CefWindowData" />.
    /// </summary>
    public class CefWindowData : INotifyPropertyChanged
    {
        #region Fields

        private bool _isAirspaceVisible;

        private bool _isFullScreen;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefWindowData"/> class.
        /// </summary>
        internal CefWindowData()
        {
            this.IsFullScreenCommand = new RelayCommand(this.OnIsFullScreen);
            this.CefContextMenuHandler = new CefContextMenuHandler();
            this.CefRequestHandler = new CefRequestHandler();
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
        /// Gets the CefContextMenuHandler.
        /// </summary>
        public CefContextMenuHandler CefContextMenuHandler { get; }

        /// <summary>
        /// Gets the CefRequestHandler.
        /// </summary>
        public CefRequestHandler CefRequestHandler { get; }

        /// <summary>
        /// Gets or sets a value indicating whether IsAirspaceVisible.
        /// </summary>
        public bool IsAirspaceVisible
        {
            get => this._isAirspaceVisible;
            set
            {
                if (this.IsMessageBoxVisible)
                {
                    return;
                }

                this.Set(this.PropertyChanged, ref this._isAirspaceVisible, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsFullScreen.
        /// </summary>
        public bool IsFullScreen { get => _isFullScreen; set => this.Set(this.PropertyChanged, ref _isFullScreen, value); }

        /// <summary>
        /// Gets the IsFullScreenCommand.
        /// </summary>
        public ICommand IsFullScreenCommand { get; }

        /// <summary>
        /// Gets a value indicating whether IsMessageBoxVisible.
        /// </summary>
        public bool IsMessageBoxVisible { get; private set; }

        /// <summary>
        /// Gets or sets the MainWindow.
        /// </summary>
        public MetroWindow MainWindow { get; internal set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ShowMessage.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="style">The style<see cref="MessageDialogStyle"/>.</param>
        /// <returns>The <see cref="Task{MessageDialogResult}"/>.</returns>
        internal async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var currentAirspaceVisible = this.IsAirspaceVisible;
            this.IsAirspaceVisible = true;
            this.IsMessageBoxVisible = true;
            var result = this.MainWindow.ShowMessageAsync(title, message, style);
            await result;
            this.IsMessageBoxVisible = false;
            if (this.IsAirspaceVisible)
            {
                this.IsAirspaceVisible = false;
            }

            return result.Result;
        }

        /// <summary>
        /// The OnIsFullScreen.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnIsFullScreen(object obj)
        {
            this.IsFullScreen = (bool)obj;
        }

        #endregion Methods
    }
}