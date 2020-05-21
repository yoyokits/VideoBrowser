namespace VideoBrowser.ViewModels
{
    using Ookii.Dialogs.Wpf;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;
    using VideoBrowser.Models;
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
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal SettingsViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.GetFolderCommand = new RelayCommand(this.OnGetFolder);
        }

        #endregion Constructors

        #region Properties

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
        public string OutputFolder { get => this._outputFolder; set => this.Set(this.PropertyChangedHandler, ref this._outputFolder, value); }

        /// <summary>
        /// Gets or sets the OutputType.
        /// </summary>
        public string OutputType { get; set; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        private GlobalData GlobalData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnGetFolder.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnGetFolder(object obj)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Download Folder Location";
            dialog.UseDescriptionForTitle = true;
            if ((bool)dialog.ShowDialog(this.GlobalData.MainWindow))
            {
                this.OutputFolder = dialog.SelectedPath;
            }
        }

        #endregion Methods
    }
}