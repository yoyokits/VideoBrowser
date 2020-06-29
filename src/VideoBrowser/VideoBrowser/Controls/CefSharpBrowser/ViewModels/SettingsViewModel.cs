namespace VideoBrowser.Controls.CefSharpBrowser.ViewModels
{
    using Ookii.Dialogs.Wpf;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;
    using VideoBrowser.Controls.CefSharpBrowser.Models;
    using VideoBrowser.Extensions;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="SettingsViewModel" />.
    /// </summary>
    public class SettingsViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string _outputFolder;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        internal SettingsViewModel()
        {
            this.GetFolderCommand = new RelayCommand(this.OnGetFolder);
            var settings = BrowserSettingsHelper.Load();
            if (settings != null)
            {
                this.BrowserSettings = settings;
            }

            var outputFolder = this.BrowserSettings.OutputFolder;
            this.OutputFolder = string.IsNullOrEmpty(outputFolder) || !Directory.Exists(outputFolder) ? AppEnvironment.UserVideoFolder : outputFolder;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BrowserSettings.
        /// </summary>
        public BrowserSettings BrowserSettings { get; } = new BrowserSettings();

        /// <summary>
        /// Gets the GetFolderCommand.
        /// </summary>
        public ICommand GetFolderCommand { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Settings;

        /// <summary>
        /// Gets or sets the OutputFolder.
        /// </summary>
        public string OutputFolder
        {
            get => this._outputFolder;
            set
            {
                this.Set(this.PropertyChangedHandler, ref this._outputFolder, value);
                if (Directory.Exists(this.OutputFolder))
                {
                    this.BrowserSettings.OutputFolder = this.OutputFolder;
                }
            }
        }

        /// <summary>
        /// Gets or sets the OutputType.
        /// </summary>
        public string OutputType { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnGetFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnGetFolder(object obj)
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Download Folder Location",
                UseDescriptionForTitle = true,
                SelectedPath = this.OutputFolder
            };

            var element = obj as FrameworkElement;
            var window = Window.GetWindow(element);
            if ((bool)dialog.ShowDialog(window))
            {
                this.OutputFolder = dialog.SelectedPath;
            }
        }

        #endregion Methods
    }
}