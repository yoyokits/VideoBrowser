namespace VideoBrowser.Models
{
    using MahApps.Metro.Controls.Dialogs;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="GlobalData" />.
    /// </summary>
    public class GlobalData : INotifyPropertyChanged
    {
        #region Fields

        private bool _isAirspaceVisible;

        private bool _isFullScreen;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        internal GlobalData()
        {
            this.DownloadQueueViewModel = new DownloadQueueViewModel();
            this.Settings = new SettingsViewModel(this);
            this.IsFullScreenCommand = new RelayCommand(this.OnIsFullScreen);
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
        /// Gets the DownloadQueueViewModel.
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel { get; }

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
        public MainWindow MainWindow { get; internal set; }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ShowMessage.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        internal void ShowMessage(string title, string message)
        {
            this.MainWindow.InvokeUIThread(() => MessageBox.Show(this.MainWindow, message, title));
        }

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