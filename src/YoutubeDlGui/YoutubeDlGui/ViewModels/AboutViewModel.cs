namespace YoutubeDlGui.ViewModels
{
    using System.Diagnostics;
    using System.Windows.Input;
    using System.Windows.Media;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Resources;

    /// <summary>
    /// Defines the <see cref="AboutViewModel" />.
    /// </summary>
    public class AboutViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        internal AboutViewModel()
        {
            this.ApplicationName = AppEnvironment.Name;
            this.ProjectUrlClickedCommand = new RelayCommand(this.OnProjectUrlClicked, nameof(this.ProjectUrlClickedCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ApplicationName.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Gets the Author.
        /// </summary>
        public string Author => $"By {AppEnvironment.Author}";

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get; set; } = Icons.Info;

        /// <summary>
        /// Gets the ProjectUrl.
        /// </summary>
        public string ProjectUrl => AppEnvironment.ProjectUrl;

        /// <summary>
        /// Gets the ProjectUrlClickedCommand.
        /// </summary>
        public ICommand ProjectUrlClickedCommand { get; }

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public string Version => $"Version {AppEnvironment.Version}";

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnProjectUrlClicked.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnProjectUrlClicked(object obj)
        {
            Process.Start(new ProcessStartInfo(this.ProjectUrl));
        }

        #endregion Methods
    }
}