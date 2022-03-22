namespace VideoBrowser.ViewModels
{
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;
    using VideoBrowser.Resources;

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
            this.ApplicationName = AppEnvironment.LongName;
            this.ProjectUrlClickedCommand = new RelayCommand(this.OnProjectUrlClicked, nameof(this.ProjectUrlClickedCommand));
            this.LinkedInCommand = new RelayCommand(this.OnLinkedIn, nameof(this.TwitterCommand));
            this.TwitterCommand = new RelayCommand(this.OnTwitter, nameof(this.TwitterCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AppIcon.
        /// </summary>
        public ImageSource AppIcon { get; } = Helpers.ImageHelper.ToImageSource(Properties.Resources.Icon);

        /// <summary>
        /// Gets the ApplicationName.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Gets the Author.
        /// </summary>
        public string Author => $"By {AppEnvironment.Author}";

        /// <summary>
        /// Gets the Icon.
        /// </summary>
        public Geometry Icon => Icons.Info;

        /// <summary>
        /// Gets the LinkedInCommand.
        /// </summary>
        public ICommand LinkedInCommand { get; }

        /// <summary>
        /// Gets the ProjectUrl.
        /// </summary>
        public string ProjectUrl => AppEnvironment.ProjectUrl;

        /// <summary>
        /// Gets the ProjectUrlClickedCommand.
        /// </summary>
        public ICommand ProjectUrlClickedCommand { get; }

        /// <summary>
        /// Gets the TwitterCommand.
        /// </summary>
        public ICommand TwitterCommand { get; }

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public string Version => $"Version {AppEnvironment.Version}";

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnLinkedIn.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLinkedIn(object obj)
        {
            ProcessHelper.OpenUrl(@"https://www.linkedin.com/in/ynurcahyo/");
        }

        /// <summary>
        /// The OnProjectUrlClicked.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnProjectUrlClicked(object obj)
        {
            ProcessHelper.OpenUrl(this.ProjectUrl);
        }

        /// <summary>
        /// The OnTwitter.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnTwitter(object obj)
        {
            ProcessHelper.OpenUrl(@"https://twitter.com/txtnurcahyo");
        }

        #endregion Methods
    }
}